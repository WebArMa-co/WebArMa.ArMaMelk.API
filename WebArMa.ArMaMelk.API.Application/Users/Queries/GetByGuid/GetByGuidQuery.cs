using Mediator;
using WebArMa.ArMaMelk.API.Application.Users.DTOs;

namespace WebArMa.ArMaMelk.API.Application.Users.Queries.GetByGuid
{
    public record GetByGuidQuery(Guid Guid) : IRequest<UserDTO>;
}
