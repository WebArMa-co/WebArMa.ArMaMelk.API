using WebArMa.ArMaMelk.API.Domain._Shared.Entities;
using WebArMa.ArMaMelk.API.Domain.Users.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Auth.Entities
{
    public class Token : EntityBase
    {
        public static Token Create(Guid userGuid, string refreshToken, DateTimeOffset expiresAt, User user)
        {
            return new Token
            {
                User = user,
                UserGuid = userGuid,
                RefreshToken = refreshToken,
                ExpiresAt = expiresAt
            };
        }
        public void Revoke()
        {
            RevokedAt = DateTimeOffset.UtcNow;
        }

        private Token()
        {
            RefreshToken = string.Empty;
            User = null!;
        }

        public string RefreshToken { get; private set; }
        public DateTimeOffset ExpiresAt { get; private set; }
        public DateTimeOffset? RevokedAt { get; private set; }
        public Guid UserGuid { get; private set; }
        public int UserId { get; private set; }
        public virtual User User { get; set; }
        public bool IsActive => RevokedAt == null && ExpiresAt > DateTimeOffset.UtcNow;
    }
}
