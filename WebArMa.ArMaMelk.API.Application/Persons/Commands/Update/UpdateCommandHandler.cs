using Mediator;
using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;

namespace WebArMa.ArMaMelk.API.Application.Persons.Commands.Update
{
    public class UpdateCommandHandler(IDatabaseContext databaseContext) : IRequestHandler<UpdateCommand>
    {
        public async ValueTask<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var person = await databaseContext.Persons.FirstOrDefaultAsync(p => p.Guid == request.Guid, cancellationToken) ?? throw new NotFoundException(entity: nameof(Person));
            person.Update(request.FirstName, request.LastName);
            await databaseContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
