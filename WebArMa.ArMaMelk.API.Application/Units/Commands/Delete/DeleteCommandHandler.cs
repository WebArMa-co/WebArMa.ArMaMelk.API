using Mediator;
using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Domain.RealStates.Entities;

namespace WebArMa.ArMaMelk.API.Application.Properties.Commands.Delete
{
    public class DeleteCommandHandler(IDatabaseContext databaseContext) : IRequestHandler<DeleteCommand>
    {
        public async ValueTask<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var property = await databaseContext.Properties.FirstOrDefaultAsync(p => p.Guid == request.Guid, cancellationToken) ?? throw new NotFoundException(nameof(Property));

            databaseContext.Properties.Remove(property);
            await databaseContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
