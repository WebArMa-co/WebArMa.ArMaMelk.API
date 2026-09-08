using Mediator;
using WebArMa.ArMaMelk.API.Application.Auth.DTOs;

namespace WebArMa.ArMaMelk.API.Application.Auth.Queries.GetByGuid
{
    public record GetByGuidQuery(Guid Guid) : IRequest<UserDTO>;
}
