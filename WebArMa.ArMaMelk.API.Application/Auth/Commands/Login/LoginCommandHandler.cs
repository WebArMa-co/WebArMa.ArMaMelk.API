using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Application._Shared.Helpers;
using WebArMa.ArMaMelk.API.Application.Redis;
using WebArMa.ArMaMelk.API.Domain.Auth.Entities;
using WebArMa.ArMaMelk.API.Domain.OTPs;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;
using WebArMa.ArMaMelk.API.Domain.Users.Entities;

namespace WebArMa.ArMaMelk.API.Application.Auth.Commands.Login
{
    public class LoginCommandHandler(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IDatabaseContext databaseContext, IRedisService redisService) : IRequestHandler<LoginCommand, Guid>
    {
        public async ValueTask<Guid> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var secret = configuration["Otp:Secret"] ?? throw new InvalidOperationException("OTP secret is not configured.");
            var maxAttemptCount = Convert.ToInt32(configuration["Otp:MaxAttemptCount"]);
            var hashedOTP = Hasher.Hash(request.Code, secret);
            var otp = await databaseContext.OTPs.OrderByDescending(o => o.CreatedAt).FirstOrDefaultAsync(o => o.UsedAt == null && o.ExpiresAt > DateTimeOffset.UtcNow && o.AttemptCount < maxAttemptCount && o.UserName == request.UserName, cancellationToken) ?? throw new NotFoundException(nameof(OTP));

            otp.IncreaseAttempt();

            if (otp.CodeHash != hashedOTP)
            {
                await databaseContext.SaveChangesAsync(cancellationToken);
                throw new ArgumentException(nameof(OTP.CodeHash));
            }

            otp.MarkAsUsed();
            await databaseContext.SaveChangesAsync(cancellationToken);

            var user = await databaseContext.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName, cancellationToken: cancellationToken);
            var person = await databaseContext.Persons.FirstOrDefaultAsync(u => u.PhoneNumber == request.UserName, cancellationToken: cancellationToken);

            if (person == null)
            {
                person = Person.Create(null, null, request.UserName);
                await databaseContext.Persons.AddAsync(person, cancellationToken);
            }

            if (user == null)
            {
                user = User.Create(request.UserName);
                await databaseContext.Users.AddAsync(user, cancellationToken);
                await databaseContext.SaveChangesAsync(cancellationToken);
            }

            await GenerateToken(user, Guid.Empty, "");

            return user.Guid;
        }

        private async Task GenerateToken(User user, Guid roleId, string accessCode)
        {
            var issuer = configuration["JWTConfig:Issuer"]!;
            var audience = configuration["JWTConfig:Audience"]!;
            var key = configuration["JWTConfig:Key"]!;
            var expires = int.Parse(configuration["JWTConfig:AccessTokenExpirationMinutes"]!);
            var refreshExpires = int.Parse(configuration["JWTConfig:RefreshTokenExpirationDays"]!);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var jti = Guid.NewGuid().ToString("N");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Guid.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(JwtRegisteredClaimNames.GivenName, user.EffectiveDisplayName),
                new Claim(JwtRegisteredClaimNames.Name, user.Person.Name),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.Person.FamilyName),
                new Claim(JwtRegisteredClaimNames.PhoneNumber, user.Person.PhoneNumber),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Picture, user.PhotoURL ?? ""),
                new Claim("token_version", user.TokenVersion.ToString()),
                new Claim("role_id", roleId.ToString()),
                new Claim("access_code", accessCode.ToString())
            };

            var accessToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expires),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            var refreshTokenArray = Guid.NewGuid();
            var refreshTokenValue = refreshTokenArray.ToString("N");

            var refreshToken = Token.Create(user.Guid, refreshTokenValue, DateTimeOffset.UtcNow.AddMinutes(refreshExpires));

            await databaseContext.Tokens.AddAsync(refreshToken);
            await databaseContext.SaveChangesAsync();

            var token = new JwtSecurityTokenHandler().WriteToken(accessToken);

            await redisService.SetAsync($"user:token-version:{user.Id}", user.TokenVersion.ToString());

            httpContextAccessor.HttpContext!.Response.Cookies.Append("access_token", token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(expires),
                    Path = "/"
                });

            httpContextAccessor.HttpContext.Response.Cookies.Append("refresh_token", refreshTokenValue,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(refreshExpires),
                    Path = "/"
                });
        }
    }
}
