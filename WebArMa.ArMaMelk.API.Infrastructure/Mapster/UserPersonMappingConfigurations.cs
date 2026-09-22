using Mapster;
using WebArMa.ArMaMelk.API.Application.UserPersons.DTOs;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;

namespace WebArMa.ArMaMelk.API.Infrastructure.Mapster
{
    public class UserPersonMappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserPerson, UserPersonDTO>().Map(dest => dest.PhoneNumber, src => src.Person.PhoneNumber);
        }
    }
}
