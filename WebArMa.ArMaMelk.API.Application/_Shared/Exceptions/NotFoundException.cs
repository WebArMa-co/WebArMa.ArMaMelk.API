namespace WebArMa.ArMaMelk.API.Application._Shared.Exceptions
{
    public sealed class NotFoundException(string? entity = null) : ClientException("NotFound", entity)
    {
    }
}
