using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Auth.Entities
{
    public class User : EntityBase
    {
        public static User Create(string userName)
        {
            return new User
            {
                UserName = userName.Trim(),
            };
        }

        public void Update(string? displayName = null, string? photoURL = null)
        {
            DisplayName = displayName?.Trim();
            PhotoURL = photoURL?.Trim();
        }

        private User()
        {
            UserName = string.Empty;
        }

        public string UserName { get; private set; }
        public string? DisplayName { get; private set; }
        public string? PhotoURL { get; private set; }
        public string EffectiveDisplayName => !string.IsNullOrWhiteSpace(DisplayName) ? DisplayName : UserName;
    }
}
