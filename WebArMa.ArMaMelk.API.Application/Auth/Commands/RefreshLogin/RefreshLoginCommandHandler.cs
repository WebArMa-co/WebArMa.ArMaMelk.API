using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Application.Auth.DTOs;
using WebArMa.ArMaMelk.API.Application.Auth.Services;

namespace WebArMa.ArMaMelk.API.Application.Auth.Commands.RefreshLogin
{
    public class RefreshLoginCommandHandler(IConfiguration configuration, IDatabaseContext databaseContext, IHttpContextAccessor httpContextAccessor, ITokenService generateTokenService) : IRequestHandler<RefreshLoginCommand, TokenDTO>
    {
        public async ValueTask<TokenDTO> Handle(RefreshLoginCommand request, CancellationToken cancellationToken)
        {
            var secret = configuration["Otp:Secret"] ?? throw new ConfigurationException("Otp:Secret");
            var maxAttemptCount = Convert.ToInt32(configuration["Otp:MaxAttemptCount"]);

            var stringUserGuid = httpContextAccessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (!Guid.TryParse(stringUserGuid, out Guid userGuid))
            {
                throw new UnauthorizedException();
            }

            var user = await databaseContext.Users.FirstOrDefaultAsync(u => u.Guid == userGuid, cancellationToken) ?? throw new UnauthorizedException();

            return await generateTokenService.GenerateAsync(user.Id, Guid.Empty, "", cancellationToken);
        }
    }
}
