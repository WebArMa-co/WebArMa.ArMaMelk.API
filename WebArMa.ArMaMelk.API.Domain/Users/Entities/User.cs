using WebArMa.ArMaMelk.API.Domain._Shared.Entities;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;
using WebArMa.ArMaMelk.API.Domain.RealStates.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Users.Entities
{
    public class User : EntityBase
    {
        public static User Create(string userName, Person person)
        {
            return new User
            {
                UserName = userName.Trim(),
                Person = person
            };
        }

        public void Update(string? displayName = null, string? photoURL = null)
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
        }

        public long TokenVersion { get; set; }
        public string UserName { get; private set; }
        public string? DisplayName { get; private set; }
        public string? PhotoURL { get; private set; }
        public string EffectiveDisplayName => !string.IsNullOrWhiteSpace(DisplayName) ? DisplayName : UserName;
        public Guid PersonGuid { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public virtual IReadOnlyCollection<Property> Properties { get; set; }
    }
}
