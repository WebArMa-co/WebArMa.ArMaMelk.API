namespace WebArMa.ArMaMelk.API.Application._Shared.Exceptions
{
    public interface IClientException
    {
        string Code { get; }
        string? Entity { get; }
    }
}
