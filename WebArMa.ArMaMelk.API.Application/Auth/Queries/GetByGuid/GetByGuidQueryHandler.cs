using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Application.Auth.DTOs;
using WebArMa.ArMaMelk.API.Domain.Auth.Entities;

namespace WebArMa.ArMaMelk.API.Application.Auth.Queries.GetByGuid
{
    public class GetByGuidQueryHandler(IDatabaseContext databaseContext, TypeAdapterConfig config) : IRequestHandler<GetByGuidQuery, UserDTO>
    {
        public async ValueTask<UserDTO> Handle(GetByGuidQuery request, CancellationToken cancellationToken)
        {
            var user = await databaseContext.Users.ProjectToType<UserDTO>(config).FirstOrDefaultAsync(u => u.Guid == request.Guid, cancellationToken) ?? throw new NotFoundException(nameof(User));
            return user;
        }
    }
}
