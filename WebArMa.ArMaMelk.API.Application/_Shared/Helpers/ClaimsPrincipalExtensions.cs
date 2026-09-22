using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;

namespace WebArMa.ArMaMelk.API.Application._Shared.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            return !Guid.TryParse(value, out var userId) ? throw new UnauthorizedException() : userId;
        }

        public static Guid GetRoleId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst("role_id")?.Value;
            return !Guid.TryParse(value, out var roleId) ? throw new UnauthorizedException() : roleId;
        }

        public static string GetJti(this ClaimsPrincipal user)
        {
            var value = user.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            return string.IsNullOrWhiteSpace(value) ? throw new UnauthorizedException() : value;
        }

        public static long GetAccessCode(this ClaimsPrincipal user)
        {
            var value = user.FindFirst("access_code")?.Value;
            return !long.TryParse(value, out var accessCode) ? throw new UnauthorizedException() : accessCode;
        }
    }
}
