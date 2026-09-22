namespace WebArMa.ArMaMelk.API.Application._Shared.Exceptions
{
    public sealed class UnauthorizedException(string? entity = null) : UnauthorizedAccessException("Unauthorized"), IClientException
    {
        public string Code => "Unauthorized";
        public string? Entity { get; } = entity;
    }
}
