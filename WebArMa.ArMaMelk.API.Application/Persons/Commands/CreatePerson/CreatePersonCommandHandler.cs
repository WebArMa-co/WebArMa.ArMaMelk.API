using Mediator;
using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;

namespace WebArMa.ArMaMelk.API.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandHandler(IDatabaseContext databaseContext) : IRequestHandler<CreatePersonCommand, Guid>
    {
        public async ValueTask<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await databaseContext.Persons.FirstOrDefaultAsync(p => p.PhoneNumber == request.PhoneNumber, cancellationToken: cancellationToken);

            if (person == null)
            {
                person = Person.Create(request.FirstName, request.LastName, request.PhoneNumber);
                await databaseContext.Persons.AddAsync(person, cancellationToken);
                await databaseContext.SaveChangesAsync(cancellationToken);
            }

            return person.Guid;
        }
    }
}
