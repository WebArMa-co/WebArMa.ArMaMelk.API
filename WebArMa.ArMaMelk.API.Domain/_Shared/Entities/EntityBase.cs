namespace WebArMa.ArMaMelk.API.Domain._Shared.Entities
{
    public class EntityBase
    {
        public void MarkDeleted()
        {
            if (IsDeleted)
            {
                return;
            }

            DeletedAt = DateTimeOffset.UtcNow;
        }
        public void Restore()
        {
            DeletedAt = null;
        }
        public void MarkUpdated(Guid? updatedByGuid = null)
        {
            UpdatedAt = DateTimeOffset.UtcNow;
            UpdatedByGuid = updatedByGuid;
        }
        public void SetCreatedBy(Guid? createdByGuid)
        {
            CreatedByGuid = createdByGuid;
        }

        public EntityBase()
        {
            Guid = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public Guid? CreatedByGuid { get; private set; }
        public Guid? UpdatedByGuid { get; private set; }
        public byte[] RowVersion { get; private set; } = [];
        public bool IsDeleted => DeletedAt.HasValue;
    }
}
