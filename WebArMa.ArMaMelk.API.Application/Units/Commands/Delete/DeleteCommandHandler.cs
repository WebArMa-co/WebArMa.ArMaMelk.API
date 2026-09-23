using Mediator;
using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Domain.RealEstates.Entities;

namespace WebArMa.ArMaMelk.API.Application.Units.Commands.Delete
{
    public class DeleteCommandHandler(IDatabaseContext databaseContext) : IRequestHandler<DeleteCommand>
    {
        public async ValueTask<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var realEstate = await databaseContext.RealEstates.FirstOrDefaultAsync(p => p.Guid == request.Guid, cancellationToken) ?? throw new NotFoundException(nameof(RealEstate));

            databaseContext.RealEstates.Remove(realEstate);
            await databaseContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
