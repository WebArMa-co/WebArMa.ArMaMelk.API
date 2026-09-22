using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Domain.Users.Entities;

namespace WebArMa.ArMaMelk.API.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(IDatabaseContext databaseContext, IHttpContextAccessor httpContextAccessor) : IRequestHandler<UpdateUserCommand>
    {
        public async ValueTask<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var stringUserGuid = httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (Guid.TryParse(stringUserGuid, out Guid userGuid))
            {
                throw new UnauthorizedAccessException();
            }

            var user = await databaseContext.Users.Include(u => u.Person).FirstOrDefaultAsync(u => u.Guid == userGuid, cancellationToken) ?? throw new NotFoundException(nameof(User));
            user.Update(request.DisplayName, request.PhotoURL);
            user.Person.Update(request.Name, request.FamilyName);
            await databaseContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
