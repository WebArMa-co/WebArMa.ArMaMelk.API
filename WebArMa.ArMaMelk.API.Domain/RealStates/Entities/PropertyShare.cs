using WebArMa.ArMaMelk.API.Domain._Shared.Entities;
using WebArMa.ArMaMelk.API.Domain.Users.Entities;

namespace WebArMa.ArMaMelk.API.Domain.RealStates.Entities
{
    public class PropertyShare : EntityBase
    {
        public static PropertyShare Create(long propertyID, User? user = null, DateTimeOffset? expiresAt = null)
        {
            return new PropertyShare
            {
                PropertyID = propertyID,
                UserGuid = user?.Guid,
                User = user,
                Token = Guid.NewGuid().ToString("N"),
                ExpiresAt = expiresAt
            };
        }
        public void RegisterView()
        {
            ViewCount++;
            LastViewedAt = DateTimeOffset.UtcNow;
        }
        public void Revoke()
        {
            RevokedAt = DateTimeOffset.UtcNow;
        }
        public bool IsValid(DateTimeOffset now)
        {
            return RevokedAt is null && (ExpiresAt is null || ExpiresAt > now);
        }

        public PropertyShare()
        {
            Property = null!;
        }

        public long PropertyID { get; private set; }
        public string Token { get; private set; } = null!;
        public Guid? UserGuid { get; private set; }
        public int? UserId { get; private set; }
        public User? User { get; private set; }
        public DateTimeOffset? ExpiresAt { get; private set; }
        public DateTimeOffset? RevokedAt { get; private set; }
        public int ViewCount { get; private set; }
        public DateTimeOffset? LastViewedAt { get; private set; }
        public virtual Property Property { get; private set; }
    }
}
