using WebArMa.ArMaMelk.API.Application._Shared.DTOs;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.DTOs
{
    public class UserPersonDTO : DTOBase
    {
        public string Name { get; set; } = string.Empty;
        public string FamilyName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Guid UserGuid { get; set; }
    }
}
