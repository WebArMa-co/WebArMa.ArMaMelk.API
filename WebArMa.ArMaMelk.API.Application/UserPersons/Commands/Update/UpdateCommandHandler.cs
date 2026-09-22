using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Commands.Update
{
    public class UpdateCommandHandler(IDatabaseContext databaseContext, IHttpContextAccessor httpContextAccessor) : IRequestHandler<UpdateCommand>
    {
        public async ValueTask<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (Guid.TryParse(currentUserId, out Guid userId))
            {
                throw new UnauthorizedAccessException();
            }

            var userPerson = await databaseContext.UserPersons.Include(u => u.Person).FirstOrDefaultAsync(u => u.Guid == request.UserPersonGuid, cancellationToken) ?? throw new NotFoundException(entity: nameof(request.UserPersonGuid));

            if (userPerson.UserGuid != userId)
            {
                throw new UnauthorizedAccessException();
            }

            var person = userPerson.Person;

            if (userPerson.Person.PhoneNumber != request.PhoneNumber)
            {
                person = await databaseContext.Persons.FirstOrDefaultAsync(p => p.PhoneNumber == request.PhoneNumber, cancellationToken);
                person ??= Person.Create(null, null, request.PhoneNumber);
            }

            userPerson.Update(request.Name, request.FamilyName, person);
            databaseContext.UserPersons.Update(userPerson);
            await databaseContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
