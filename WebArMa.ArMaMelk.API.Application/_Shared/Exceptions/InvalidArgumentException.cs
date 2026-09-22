namespace WebArMa.ArMaMelk.API.Application._Shared.Exceptions
{
    public sealed class InvalidArgumentException(string? entity = null) : ArgumentException("Invalid", entity), IClientException
    {
        public string Code => "Invalid";
        public string? Entity { get; } = entity;
    }
}
