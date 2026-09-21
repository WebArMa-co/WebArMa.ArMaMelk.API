using Mediator;
using WebArMa.ArMaMelk.API.Application.Auth.DTOs;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;
using WebArMa.ArMaMelk.API.Domain.Users.Entities;

namespace WebArMa.ArMaMelk.API.Application.Auth.Commands.RefreshLogin
{
    public record RefreshLoginCommand() : IRequest<TokenDTO>;
}
