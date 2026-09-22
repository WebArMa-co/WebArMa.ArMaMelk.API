using Mediator;

namespace WebArMa.ArMaMelk.API.Application.Users.Commands.Update
{
    public record UpdateCommand(string Name, string FamilyName, string? DisplayName, string? PhotoURL) : IRequest;
}
