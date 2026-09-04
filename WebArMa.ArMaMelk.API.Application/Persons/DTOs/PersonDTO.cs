using WebArMa.ArMaMelk.API.Application._Shared.DTOs;

namespace WebArMa.ArMaMelk.API.Application.Persons.DTOs
{
    public class PersonDTO : DTOBase
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
