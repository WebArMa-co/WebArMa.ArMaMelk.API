using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Persons.Entities
{
    public class Person : EntityBase
    {
        private Person()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            PhoneNumber = string.Empty;
        }

        public static Person Create(string firstName, string lastName, string phoneNumber)
        {
            return new Person { FirstName = firstName.Trim(), LastName = lastName.Trim(), PhoneNumber = phoneNumber.Trim() };
        }

        public void Update(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}
