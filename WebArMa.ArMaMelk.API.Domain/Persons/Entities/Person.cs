using WebArMa.ArMaMelk.API.Domain._Shared.Entities;
using WebArMa.ArMaMelk.API.Domain.RealStates.Entities;
using WebArMa.ArMaMelk.API.Domain.Users.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Persons.Entities
{
    public class Person : EntityBase
    {
        public static Person Create(string phoneNumber)
        {
            return new Person { PhoneNumber = phoneNumber.Trim() };
        }
        public void Update(string name, string familyName)
        {
            Name = name.Trim();
            FamilyName = familyName.Trim();
        }

        private Person()
        {
            Name = string.Empty;
            FamilyName = string.Empty;
            PhoneNumber = string.Empty;
            Users = [];
            Properties = [];
        }

        public string Name { get; private set; }
        public string FamilyName { get; private set; }
        public string PhoneNumber { get; private set; }
        public virtual IReadOnlyCollection<User> Users { get; set; }
        public virtual IReadOnlyCollection<Property> Properties { get; set; }
    }
}
