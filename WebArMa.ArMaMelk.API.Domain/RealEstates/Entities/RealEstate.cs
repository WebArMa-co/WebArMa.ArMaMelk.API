using WebArMa.ArMaMelk.API.Domain._Shared.Entities;
using WebArMa.ArMaMelk.API.Domain.Locations.Entities;
using WebArMa.ArMaMelk.API.Domain.RealEstates.Enums;

namespace WebArMa.ArMaMelk.API.Domain.RealEstates.Entities
{
    public class RealEstate : EntityBase
    {
        public static RealEstate Create(string title, RealEstateType realStateType, UsageType usageType, Address address, Specification specifications, Features features, RealEstateOwnership realStateOwnership, Building? building = null)
        {
            return new RealEstate
            {
                Title = title.Trim(),
                RealEstateType = realStateType,
                UsageType = usageType,
                Status = RealEstateStatus.Active,
                Address = address,
                Specifications = specifications,
                Features = features,
                RealEstateOwnership = realStateOwnership,
                Building = building
            };
        }
        public void Update(string title, RealEstateType realStateType, UsageType usageType, Address address, Specification specifications, Features features, RealEstateOwnership realStateOwnership, Building? building = null)
        {
            Title = title.Trim();
            RealEstateType = realStateType;
            UsageType = usageType;
            Status = RealEstateStatus.Active;
            Address = address;
            Specifications = specifications;
            Features = features;
            RealEstateOwnership = realStateOwnership;
            Building = building;
        }

        private RealEstate()
        {
            Title = null!;
            Address = null!;
            Specifications = null!;
            Features = null!;
            RealEstateOwnership = null!;
        }

        public string Title { get; private set; }
        public RealEstateType RealEstateType { get; private set; }
        public UsageType UsageType { get; private set; }
        public RealEstateStatus Status { get; private set; }
        public virtual Address Address { get; private set; }
        public virtual Specification Specifications { get; private set; }
        public virtual Features Features { get; private set; }
        public virtual RealEstateOwnership RealEstateOwnership { get; private set; }
        public int? BuildingId { get; private set; }
        public virtual Building? Building { get; private set; }
    }
}
