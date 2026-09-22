namespace WebArMa.ArMaMelk.API.Application._Shared.Exceptions
{
    public abstract class ClientException(string code, string? entity = null) : Exception(code), IClientException
    {
        public string Code { get; } = code;
        public string? Entity { get; } = entity;
    }
}
