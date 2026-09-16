using Mediator;

namespace WebArMa.ArMaMelk.API.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(Guid Guid, string DisplayName, string PhotoURL) : IRequest;
}
