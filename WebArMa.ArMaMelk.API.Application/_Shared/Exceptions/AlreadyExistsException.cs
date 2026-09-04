namespace WebArMa.ArMaMelk.API.Application._Shared.Exceptions
{
    public sealed class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string entity, string? message = null) : base(message ?? $"{entity} already exists.")
        {
            Entity = entity;
        }

        public AlreadyExistsException(string message)
            : base(message)
        {
        }

        public AlreadyExistsException()
        {
        }

        public string? Entity { get; }
    }
}
