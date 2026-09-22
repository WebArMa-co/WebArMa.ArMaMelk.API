using Mediator;

namespace WebArMa.ArMaMelk.API.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(string Name, string FamilyName, string? DisplayName, string? PhotoURL) : IRequest;
}
