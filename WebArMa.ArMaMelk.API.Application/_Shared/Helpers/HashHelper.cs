using System.Security.Cryptography;
using System.Text;

namespace WebArMa.ArMaMelk.API.Application._Shared.Helpers
{
    public static class HashHelper
    {
        public static string Hash(string otp, string secret)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secret);

            using var hmac = new HMACSHA256(secretBytes);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(otp));
            return Convert.ToHexString(hash);
        }
    }
}
