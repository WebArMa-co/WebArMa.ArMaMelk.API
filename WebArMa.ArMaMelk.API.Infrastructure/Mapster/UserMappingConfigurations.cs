using Mapster;
using WebArMa.ArMaMelk.API.Application.Persons.DTOs;
using WebArMa.ArMaMelk.API.Application.Users.DTOs;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;
using WebArMa.ArMaMelk.API.Domain.Users.Entities;

namespace WebArMa.ArMaMelk.API.Infrastructure.Mapster
{
    public class UserMappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserDTO>().Map(des => des.Name, src => src.Person.Name)
                .Map(des => des.FamilyName, src => src.Person.FamilyName)
                .Map(des => des.PhoneNumber, src => src.Person.PhoneNumber);
        }
    }
}
