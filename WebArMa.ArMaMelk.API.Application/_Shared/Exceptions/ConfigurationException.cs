namespace WebArMa.ArMaMelk.API.Application._Shared.Exceptions
{
    public sealed class ConfigurationException(string? entity = null) : InvalidOperationException("Configuration"), IClientException
    {
        public string Code => "Configuration";
        public string? Entity { get; } = entity;
    }
}
