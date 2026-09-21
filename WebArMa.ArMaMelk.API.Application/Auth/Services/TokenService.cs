using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application.Auth.DTOs;
using WebArMa.ArMaMelk.API.Application.Redis;
using WebArMa.ArMaMelk.API.Domain.Auth.Entities;

namespace WebArMa.ArMaMelk.API.Application.Auth.Services
{
    public class TokenService(IConfiguration configuration, IDatabaseContext databaseContext, IRedisService redisService) : ITokenService
    {
        public async Task<TokenDTO> GenerateAsync(int userId, Guid roleId, string accessCode, CancellationToken cancellationToken)
        {
            var issuer = configuration["JWTConfig:Issuer"]!;
            var audience = configuration["JWTConfig:Audience"]!;
            var key = configuration["JWTConfig:Key"]!;
            var expires = int.Parse(configuration["JWTConfig:AccessTokenExpirationMinutes"]!);
            var refreshExpires = int.Parse(configuration["JWTConfig:RefreshTokenExpirationDays"]!);

            var user = await databaseContext.Users.Include(u => u.Person).FirstOrDefaultAsync(u => u.Id == userId, cancellationToken) ?? throw new UnauthorizedAccessException();
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

            var refreshToken = Token.Create(user.Guid, refreshTokenValue, DateTimeOffset.UtcNow.AddMinutes(refreshExpires), user);

            await databaseContext.Tokens.AddAsync(refreshToken, cancellationToken);
            await databaseContext.SaveChangesAsync(cancellationToken);

            await redisService.SetAsync($"user:token-version:{user.Guid}", user.TokenVersion.ToString());
            var token = new JwtSecurityTokenHandler().WriteToken(accessToken);

            return new TokenDTO { Token = token, RefreshToken = refreshTokenValue };
        }
    }
}
