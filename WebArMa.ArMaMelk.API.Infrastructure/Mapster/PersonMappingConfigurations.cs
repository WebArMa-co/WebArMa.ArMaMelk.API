using Mapster;
using WebArMa.ArMaMelk.API.Application.Persons.DTOs;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;

namespace WebArMa.ArMaMelk.API.Infrastructure.Mapster
{
    public class PersonMappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Person, PersonDTO>();
        }
    }
}
