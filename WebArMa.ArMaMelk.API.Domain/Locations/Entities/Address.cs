using NetTopologySuite.Geometries;
using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Locations.Entities
{
    public class Address : EntityBase
    {
        public static Address Create(Village village, string systemAddress, string addressLine, Point? location = null)
        {
            return new Address
            {
                Village = village,
                SystemAddress = systemAddress,
                AddressLine = addressLine,
                Location = location
            };
        }
        public void Update(Village village, string systemAddress, string addressLine, Point? location = null)
        {
            Village = village;
            SystemAddress = systemAddress;
            AddressLine = addressLine;
            Location = location;
        }

        private Address()
        {
            SystemAddress = null!;
            AddressLine = null!;
            Village = null!;
        }

        public string SystemAddress { get; private set; }
        public string AddressLine { get; private set; }
        public Point? Location { get; private set; }
        public int VillageId { get; private set; }
        public Village Village { get; private set; }
    }
}
