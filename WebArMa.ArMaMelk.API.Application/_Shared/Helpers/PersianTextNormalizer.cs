using Microsoft.Extensions.Configuration;

namespace WebArMa.ArMaMelk.API.Application._Shared.Helpers
{
    public static class PersianTextNormalizer
    {
        public static string Normalize(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value
                .Replace('ي', 'ی')
                .Replace('ى', 'ی')
                .Replace('ك', 'ک');
        }
    }
}
