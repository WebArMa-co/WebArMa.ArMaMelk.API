using Mediator;

namespace WebArMa.ArMaMelk.API.Application.Auth.Commands.Logout
{
    public record LogoutCommand(bool TerminateAllSessions) : IRequest;
}
