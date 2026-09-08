using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Application.Persons.DTOs;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;

namespace WebArMa.ArMaMelk.API.Application.Persons.Queries.GetPersonByGuid
{
    public class GetByGuidQueryHandler(IDatabaseContext databaseContext, TypeAdapterConfig config) : IRequestHandler<GetByGuidQuery, PersonDTO>
    {
        public async ValueTask<PersonDTO> Handle(GetByGuidQuery request, CancellationToken cancellationToken)
        {
            var person = await databaseContext.Persons.ProjectToType<PersonDTO>(config).FirstOrDefaultAsync(p => p.Guid == request.Guid, cancellationToken: cancellationToken) ?? throw new NotFoundException(entity: nameof(Person));
            return person;
        }
    }
}
