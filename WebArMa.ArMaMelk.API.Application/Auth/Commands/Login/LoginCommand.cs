using Mediator;

namespace WebArMa.ArMaMelk.API.Application.Auth.Commands.Login
{
    public record LoginCommand(string UserName, string Code) : IRequest<Guid>;
}
