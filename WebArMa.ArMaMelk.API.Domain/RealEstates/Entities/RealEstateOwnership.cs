using WebArMa.ArMaMelk.API.Domain._Shared.Entities;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;
using WebArMa.ArMaMelk.API.Domain.RealEstates.Enums;

namespace WebArMa.ArMaMelk.API.Domain.RealEstates.Entities
{
    public class RealEstateOwnership : EntityBase
    {
        public static RealEstateOwnership Create(Person person, RealEstateDocumentType documentType, RealEstateDocumentStatus documentStatus, RealEstateOwnershipType ownershipType)
        {
            return new RealEstateOwnership
            {
                Person = person,
                DocumentType = documentType,
                DocumentStatus = documentStatus,
                OwnershipType = ownershipType
            };
        }
        public void Update(Person person, RealEstateDocumentType documentType, RealEstateDocumentStatus documentStatus, RealEstateOwnershipType ownershipType)
        {
            Person = person;
            DocumentType = documentType;
            DocumentStatus = documentStatus;
            OwnershipType = ownershipType;
        }

        private RealEstateOwnership()
        {
            Person = null!;
        }

        public RealEstateDocumentType DocumentType { get; private set; }
        public RealEstateDocumentStatus DocumentStatus { get; private set; }
        public RealEstateOwnershipType OwnershipType { get; private set; }
        public Guid PersonGuid { get; private set; }
        public int PersonId { get; private set; }
        public virtual Person Person { get; private set; }
    }
}
