using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Helpers;
using WebArMa.ArMaMelk.API.Application.Redis;

namespace WebArMa.ArMaMelk.API.Application.Auth.Commands.Logout
{
    public class LogoutCommandHandler(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IDatabaseContext databaseContext, IRedisService redisService) : IRequestHandler<LogoutCommand>
    {
        public async ValueTask<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext.User.GetUserId();
            var user = await databaseContext.Users.FirstOrDefaultAsync(u => u.Guid == userId, cancellationToken) ?? throw new UnauthorizedAccessException();

            if (request.TerminateAllSessions)
            {
                user.RevokeAllTokens();
                await redisService.SetAsync($"user:token-version:{user.Id}", user.TokenVersion.ToString());
                await databaseContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                var expires = int.Parse(configuration["JWTConfig:expires"]!);
                var jti = httpContextAccessor.HttpContext.User.GetJti();
                await redisService.SetAsync($"jwt:revoked:{jti}", "1", TimeSpan.FromMinutes(expires));
            }

            return Unit.Value;
        }
    }
}
