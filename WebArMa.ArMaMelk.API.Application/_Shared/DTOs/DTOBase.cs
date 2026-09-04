namespace WebArMa.ArMaMelk.API.Application._Shared.DTOs
{
    public class DTOBase
    {
        public Guid Guid { get; protected set; }
        public DateTimeOffset CreatedAt { get; protected set; }
        public DateTimeOffset? UpdatedAt { get; protected set; }
        public DateTimeOffset? DeletedAt { get; protected set; }
        public Guid? CreatedByGuid { get; protected set; }
        public Guid? UpdatedByGuid { get; protected set; }
        public byte[] RowVersion { get; private set; } = [];
        public bool IsDeleted => DeletedAt.HasValue;
    }
}
