using Mediator;
using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;

namespace WebArMa.ArMaMelk.API.Application.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler(IDatabaseContext databaseContext) : IRequestHandler<UpdatePersonCommand>
    {
        public async ValueTask<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await databaseContext.Persons.FirstOrDefaultAsync(p => p.Guid == request.Guid, cancellationToken) ?? throw new NotFoundException(entity: nameof(Person));
            person.Update(request.FirstName, request.LastName);
            await databaseContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
