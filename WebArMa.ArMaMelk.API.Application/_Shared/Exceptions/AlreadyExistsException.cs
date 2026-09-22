namespace WebArMa.ArMaMelk.API.Application._Shared.Exceptions
{
    public sealed class AlreadyExistsException(string? entity = null) : ClientException("AlreadyExists", entity)
    {
    }
}
