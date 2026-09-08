using Mediator;
using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Domain.Users.Entities;

namespace WebArMa.ArMaMelk.API.Application.Users.Commands.Update
{
    public class UpdateCommandHandler(IDatabaseContext databaseContext) : IRequestHandler<UpdateCommand>
    {
        public async ValueTask<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var user = await databaseContext.Users.FirstOrDefaultAsync(u => u.Guid == request.Guid, cancellationToken) ?? throw new NotFoundException(nameof(User));
            user.Update(request.DisplayName, request.PhotoURL);
            await databaseContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
