using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Commands.Create
{
    public class CreateCommandHandler(IDatabaseContext databaseContext, IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateCommand>
    {
        public async ValueTask<Unit> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var stringUserGuid = httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (!Guid.TryParse(stringUserGuid, out Guid userGuid))
            {
                throw new UnauthorizedAccessException();
            }

            var userPerson = await databaseContext.UserPersons.Include(u => u.Person).FirstOrDefaultAsync(u => u.UserGuid == userGuid && u.Person.PhoneNumber == request.PhoneNumber, cancellationToken);

            if (userPerson != null)
            {
                throw new AlreadyExistsException(nameof(request.PhoneNumber));
            }

            var person = await databaseContext.Persons.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken);
            person ??= Person.Create(null, null, request.PhoneNumber);

            userPerson = UserPerson.Create(request.Name, request.FamilyName, person);

            await databaseContext.UserPersons.AddAsync(userPerson, cancellationToken);
            await databaseContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
