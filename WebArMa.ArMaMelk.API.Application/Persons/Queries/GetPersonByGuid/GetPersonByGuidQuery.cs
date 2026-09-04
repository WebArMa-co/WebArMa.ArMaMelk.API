using Mediator;
using WebArMa.ArMaMelk.API.Application.Persons.DTOs;

namespace WebArMa.ArMaMelk.API.Application.Persons.Queries.GetPersonByGuid
{
    public record GetPersonByGuidQuery(Guid Guid) : IRequest<PersonDTO>;
}
