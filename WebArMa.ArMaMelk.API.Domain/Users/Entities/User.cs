using WebArMa.ArMaMelk.API.Domain._Shared.Entities;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;
using WebArMa.ArMaMelk.API.Domain.RealEstates.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Users.Entities
{
    public class User : EntityBase
    {
        public static User Create(string userName, Person person)
        {
            return new User
            {
                UserName = userName.Trim(),
                Person = person,
                PersonGuid = person.Guid,
            };
        }
        public void Update(string? displayName, string? photoURL)
        {
            DisplayName = displayName?.Trim();
            PhotoURL = photoURL?.Trim();
        }
        public void RevokeAllTokens()
        {
            TokenVersion++;
        }

        private User()
        {
            UserName = string.Empty;
            Person = null!;
            Properties = [];
            Roles = [];
        }

        public long TokenVersion { get; private set; }
        public string UserName { get; private set; }
        public string? DisplayName { get; private set; }
        public string? PhotoURL { get; private set; }
        public Guid PersonGuid { get; private set; }
        public int PersonId { get; private set; }
        public virtual Person Person { get; private set; }
        public virtual ICollection<RealEstate> Properties { get; private set; }
        public virtual ICollection<Role> Roles { get; private set; }
    }
}
