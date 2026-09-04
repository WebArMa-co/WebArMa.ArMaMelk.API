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
using WebArMa.ArMaMelk.API.Domain.Auth.Entities;
using WebArMa.ArMaMelk.API.Domain.OTPs;

namespace WebArMa.ArMaMelk.API.Application.Auth.Commands.Login
{
    public class LoginCommandHandler(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IDatabaseContext databaseContext) : IRequestHandler<LoginCommand, Guid>
    {
        public async ValueTask<Guid> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var secret = configuration["Otp:Secret"] ?? throw new InvalidOperationException("OTP secret is not configured.");
            var hashedOTP = HashHelper.Hash(request.Code, secret);
            var otp = await databaseContext.OTPs.FirstOrDefaultAsync(o => !o.IsUsed && o.IsActive && o.UserName == request.UserName && o.CodeHash == hashedOTP, cancellationToken) ?? throw new NotFoundException(nameof(OTP));
            otp.MarkAsUsed();

            var user = await databaseContext.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName, cancellationToken: cancellationToken);

            if (user == null)
            {
                user = User.Create(request.UserName);
                await databaseContext.Users.AddAsync(user, cancellationToken);
                await databaseContext.SaveChangesAsync(cancellationToken);
            }

            await GenerateToken(user.Guid, Guid.Empty, "");

            return user.Guid;
        }

        private async Task GenerateToken(Guid userGuid, Guid roleId, string accessCode)
        {
            var issuer = configuration["JWTConfig:issuer"]!;
            var audience = configuration["JWTConfig:audience"]!;
            var key = configuration["JWTConfig:key"]!;
            var expires = int.Parse(configuration["JWTConfig:expires"]!);
            var refreshExpires = int.Parse(configuration["JWTConfig:refreshExpires"]!);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier, userGuid.ToString()),
                 new Claim(ClaimTypes.Role, roleId.ToString()),
                 new Claim(ClaimTypes.Rsa, accessCode.ToString())
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

            var refreshToken = Token.Create(userGuid, refreshTokenValue, DateTimeOffset.UtcNow.AddMinutes(refreshExpires));

            await databaseContext.Tokens.AddAsync(refreshToken);
            await databaseContext.SaveChangesAsync();

            var token = new JwtSecurityTokenHandler().WriteToken(accessToken);

            httpContextAccessor.HttpContext!.Response.Cookies.Append(
                "access_token",
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(expires),
                    Path = "/"
                });

            httpContextAccessor.HttpContext.Response.Cookies.Append(
                "refresh_token",
                refreshTokenValue,
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
