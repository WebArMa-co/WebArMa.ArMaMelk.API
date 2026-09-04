using System.Text.RegularExpressions;

namespace WebArMa.ArMaMelk.API.Application._Shared.Helpers
{
    public static class IranianPhoneNumber
    {
        private static readonly Regex Pattern = new(@"^09\d{9}$", RegexOptions.Compiled);

        public static bool IsValidPhoneNumber(this string? phoneNumber)
        {
            return !string.IsNullOrWhiteSpace(phoneNumber) && Pattern.IsMatch(phoneNumber);
        }
    }
}
