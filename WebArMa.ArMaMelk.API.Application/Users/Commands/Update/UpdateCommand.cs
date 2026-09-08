using Mediator;

namespace WebArMa.ArMaMelk.API.Application.Users.Commands.Update
{
    public record UpdateCommand(Guid Guid, string DisplayName, string PhotoURL) : IRequest;
}
