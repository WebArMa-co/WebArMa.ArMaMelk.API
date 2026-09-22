using Mediator;
using WebArMa.ArMaMelk.API.Application.Auth.DTOs;

namespace WebArMa.ArMaMelk.API.Application.Auth.Commands.RefreshLogin
{
    public record RefreshLoginCommand() : IRequest<TokenDTO>;
}
