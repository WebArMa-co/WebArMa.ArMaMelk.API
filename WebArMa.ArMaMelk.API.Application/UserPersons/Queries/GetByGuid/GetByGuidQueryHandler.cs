using Mapster;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Application.UserPersons.DTOs;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Queries.GetByGuid
{
    public class GetByGuidQueryHandler(IDatabaseContext databaseContext, IHttpContextAccessor httpContextAccessor, TypeAdapterConfig config) : IRequestHandler<GetByGuidQuery, UserPersonDTO>
    {
        public async ValueTask<UserPersonDTO> Handle(GetByGuidQuery request, CancellationToken cancellationToken)
        {
            var stringUserGuid = httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (Guid.TryParse(stringUserGuid, out Guid userGuid))
            {
                throw new UnauthorizedAccessException();
            }

            return await databaseContext.UserPersons.ProjectToType<UserPersonDTO>(config).FirstOrDefaultAsync(u => u.Guid == request.Guid && u.UserGuid == userGuid) ?? throw new NotFoundException(entity: nameof(request.Guid));
        }
    }
}
