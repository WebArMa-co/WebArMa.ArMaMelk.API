using Mediator;
using WebArMa.ArMaMelk.API.Application.UserPersons.DTOs;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Queries.GetByGuid
{
    public record GetByGuidQuery(Guid Guid) : IRequest<UserPersonDTO>;
}
