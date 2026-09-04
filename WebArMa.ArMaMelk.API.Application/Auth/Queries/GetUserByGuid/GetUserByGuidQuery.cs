using Mediator;
using WebArMa.ArMaMelk.API.Application.Auth.DTOs;

namespace WebArMa.ArMaMelk.API.Application.Auth.Queries.GetUserByGuid
{
    public record GetUserByGuidQuery(Guid Guid) : IRequest<UserDTO>;
}
