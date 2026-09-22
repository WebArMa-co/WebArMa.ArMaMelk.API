using WebArMa.ArMaMelk.API.Domain._Shared.Entities;
using WebArMa.ArMaMelk.API.Domain.Users.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Persons.Entities
{
    public class UserPerson : EntityBase
    {
        public static UserPerson Create(string name, string familyName, Person person)
        {
            return new UserPerson { Name = name?.Trim() ?? string.Empty, FamilyName = familyName?.Trim() ?? string.Empty, Person = person };
        }
        public void Update(string name, string familyName, Person person)
        {
            Name = name.Trim();
            FamilyName = familyName.Trim();
            Person = person;
        }

        public UserPerson()
        {
            Name = string.Empty;
            FamilyName = string.Empty;
            Person = null!;
            User = null!;
        }

        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string EffectiveName => !string.IsNullOrWhiteSpace(Person?.Name) ? Person.Name : Name;
        public string EffectiveFamilyName => !string.IsNullOrWhiteSpace(Person?.FamilyName) ? Person.FamilyName : FamilyName;
        public int PersonId { get; set; }
        public int UserId { get; set; }
        public Guid UserGuid { get; set; }
        public virtual Person Person { get; set; }
        public virtual User User { get; set; }
    }
}
