using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Auth.Entities
{
    public class Token : EntityBase
    {
        public static Token Create(Guid userGuid, string refreshToken, DateTimeOffset expiresAt)
        {
            return new Token
            {
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
        }

        public Guid UserGuid { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTimeOffset ExpiresAt { get; private set; }
        public DateTimeOffset? RevokedAt { get; private set; }

        public bool IsActive => RevokedAt == null && ExpiresAt > DateTimeOffset.UtcNow;
    }
}