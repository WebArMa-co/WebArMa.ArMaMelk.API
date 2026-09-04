namespace WebArMa.ArMaMelk.API.Application._Shared.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string entity, string? message = null) : base(message ?? $"{entity} not found.")
        {
            Entity = entity;
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException()
        {
        }

        public string? Entity { get; }
    }
}
