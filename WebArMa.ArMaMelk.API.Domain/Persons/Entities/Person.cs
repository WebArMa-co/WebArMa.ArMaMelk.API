using WebArMa.ArMaMelk.API.Domain._Shared.Entities;
using WebArMa.ArMaMelk.API.Domain.Users.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Persons.Entities
{
    public class Person : EntityBase
    {
        private Person()
        {
            Name = string.Empty;
            FamilyName = string.Empty;
            PhoneNumber = string.Empty;
            Users = [];
        }

        public static Person Create(string name, string familyName, string phoneNumber)
        {
            return new Person { Name = name.Trim(), FamilyName = familyName.Trim(), PhoneNumber = phoneNumber.Trim() };
        }

        public void Update(string name, string familyName)
        {
            Name = name;
            FamilyName = familyName;
        }

        public string Name { get; private set; }
        public string FamilyName { get; private set; }
        public string PhoneNumber { get; private set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
