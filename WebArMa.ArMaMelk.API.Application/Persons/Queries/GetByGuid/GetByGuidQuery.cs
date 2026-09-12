using Mediator;
using WebArMa.ArMaMelk.API.Application.Persons.DTOs;

namespace WebArMa.ArMaMelk.API.Application.Persons.Queries.GetByGuid
{
    public record GetByGuidQuery(Guid Guid) : IRequest<PersonDTO>;
}
