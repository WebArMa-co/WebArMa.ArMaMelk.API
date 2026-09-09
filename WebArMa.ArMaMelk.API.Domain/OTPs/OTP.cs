using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.OTPs
{
    public class OTP : EntityBase
    {
        public static OTP Create(string userName, string codeHash, DateTimeOffset expiresAt)
        {
            return new OTP
            {
                UserName = userName.Trim(),
                CodeHash = codeHash,
                ExpiresAt = expiresAt
            };
        }
        public bool HasExceededAttempts(int maxAttempts) => AttemptCount >= maxAttempts;
        public bool IsActive(int maxAttempts) => !IsUsed && !IsExpired && !HasExceededAttempts(maxAttempts);
        public void IncreaseAttempt()
        {
            AttemptCount++;
        }
        public void MarkAsUsed()
        {
            UsedAt = DateTimeOffset.UtcNow;
        }
        
        private OTP()
        {
            UserName = string.Empty;
            CodeHash = string.Empty;
        }

        public string UserName { get; private set; }
        public string CodeHash { get; private set; }
        public DateTimeOffset ExpiresAt { get; private set; }
        public int AttemptCount { get; private set; }
        public DateTimeOffset? UsedAt { get; private set; }
        public bool IsExpired => ExpiresAt <= DateTimeOffset.UtcNow;
        public bool IsUsed => UsedAt.HasValue;
    }
}
