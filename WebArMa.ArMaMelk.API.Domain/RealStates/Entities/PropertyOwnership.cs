using WebArMa.ArMaMelk.API.Domain._Shared.Entities;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;
using WebArMa.ArMaMelk.API.Domain.RealStates.Enums;

namespace WebArMa.ArMaMelk.API.Domain.RealStates.Entities
{
    public class PropertyOwnership : EntityBase
    {
        public static PropertyOwnership Create(Person person, PropertyDocumentType documentType, PropertyDocumentStatus documentStatus, PropertyOwnershipType ownershipType)
        {
            return new PropertyOwnership
            {
                Person = person,
                DocumentType = documentType,
                DocumentStatus = documentStatus,
                OwnershipType = ownershipType
            };
        }
        public void Update(Person person, PropertyDocumentType documentType, PropertyDocumentStatus documentStatus, PropertyOwnershipType ownershipType)
        {
            Person = person;
            DocumentType = documentType;
            DocumentStatus = documentStatus;
            OwnershipType = ownershipType;
        }

        private PropertyOwnership()
        {
            Person = null!;
        }

        public PropertyDocumentType DocumentType { get; private set; }
        public PropertyDocumentStatus DocumentStatus { get; private set; }
        public PropertyOwnershipType OwnershipType { get; private set; }
        public Guid PersonGuid { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; private set; }
    }
}
