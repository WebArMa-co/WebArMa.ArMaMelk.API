using WebArMa.ArMaMelk.API.Domain._Shared.Entities;
using WebArMa.ArMaMelk.API.Domain.RealStates.Enums;

namespace WebArMa.ArMaMelk.API.Domain.RealStates.Entities
{
    public class Property : EntityBase
    {
        public static Property Create(string title, PropertyType propertyType, UsageType usageType, Address address, Specifications specifications, Features features, PropertyOwnership propertyOwnership, Building? building = null)
        {
            return new Property
            {
                Title = title.Trim(),
                PropertyType = propertyType,
                UsageType = usageType,
                Status = PropertyStatus.Active,
                Address = address,
                Specifications = specifications,
                Features = features,
                PropertyOwnership = propertyOwnership,
                Building = building
            };
        }
        public void Update(string title, PropertyType propertyType, UsageType usageType, Address address, Specifications specifications, Features features, PropertyOwnership propertyOwnership, Building? building = null)
        {
            Title = title.Trim();
            PropertyType = propertyType;
            UsageType = usageType;
            Status = PropertyStatus.Active;
            Address = address;
            Specifications = specifications;
            Features = features;
            PropertyOwnership = propertyOwnership;
            Building = building;
        }

        private Property()
        {
            Title = null!;
            Address = null!;
            Specifications = null!;
            Features = null!;
            PropertyOwnership = null!;
        }

        public string Title { get; private set; }
        public PropertyType PropertyType { get; private set; }
        public UsageType UsageType { get; private set; }
        public PropertyStatus Status { get; private set; }
        public virtual Address Address { get; private set; }
        public virtual Specifications Specifications { get; private set; }
        public virtual Features Features { get; private set; }
        public virtual PropertyOwnership PropertyOwnership { get; private set; }
        public int? BuildingId { get; private set; }
        public virtual Building? Building { get; private set; }
    }
}
