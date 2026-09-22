using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Commands.Delete
{
    public class DeleteCommandHandler(IDatabaseContext databaseContext, IHttpContextAccessor httpContextAccessor) : IRequestHandler<DeleteCommand>
    {
        public async ValueTask<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var stringUserGuid = httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (!Guid.TryParse(stringUserGuid, out Guid userGuid))
            {
                throw new UnauthorizedException();
            }

            var userPerson = await databaseContext.UserPersons.FirstOrDefaultAsync(u => u.Guid == request.UserPersonGuid && u.UserGuid == userGuid) ?? throw new NotFoundException(nameof(UserPerson));
            databaseContext.UserPersons.Remove(userPerson);
            await databaseContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}

