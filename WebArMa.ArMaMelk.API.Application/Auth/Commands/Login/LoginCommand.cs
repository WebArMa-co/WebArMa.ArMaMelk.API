using Mediator;
using WebArMa.ArMaMelk.API.Application.Auth.DTOs;

namespace WebArMa.ArMaMelk.API.Application.Auth.Commands.Login
{
    public record LoginCommand(string UserName, string Code) : IRequest<TokenDTO>;
}
