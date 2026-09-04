using WebArMa.ArMaMelk.API.Domain.RealStates.Enums;

namespace WebArMa.ArMaMelk.API.Domain.RealStates.Entities
{
    public sealed class PropertyOwnership
    {
        public static PropertyOwnership Create(PropertyDocumentType documentType, PropertyDocumentStatus documentStatus, PropertyOwnershipType ownershipType)
        {
            return new PropertyOwnership
            {
                DocumentType = documentType,
                DocumentStatus = documentStatus,
                OwnershipType = ownershipType
            };
        }

        public void Update(PropertyDocumentType documentType, PropertyDocumentStatus documentStatus, PropertyOwnershipType ownershipType)
        {
            DocumentType = documentType;
            DocumentStatus = documentStatus;
            OwnershipType = ownershipType;
        }

        private PropertyOwnership()
        {
        }

        public PropertyDocumentType DocumentType { get; private set; }
        public PropertyDocumentStatus DocumentStatus { get; private set; }
        public PropertyOwnershipType OwnershipType { get; private set; }
    }
}
