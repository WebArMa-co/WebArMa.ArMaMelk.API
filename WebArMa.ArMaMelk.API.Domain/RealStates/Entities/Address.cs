using NetTopologySuite.Geometries;
using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.RealStates.Entities
{
    public class Address : EntityBase
    {
        public static Address Create(Neighborhood neighborhood, string systemAddress, string addressLine, Point? location = null)
        {
            return new Address
            {
                Neighborhood = neighborhood,
                SystemAddress = systemAddress,
                AddressLine = addressLine,
                Location = location
            };
        }
        public void Update(Neighborhood neighborhood, string systemAddress, string addressLine, Point? location = null)
        {
            Neighborhood = neighborhood;
            SystemAddress = systemAddress;
            AddressLine = addressLine;
            Location = location;
        }

        private Address()
        {
            SystemAddress = null!;
            AddressLine = null!;
            Neighborhood = null!;
        }

        public string SystemAddress { get; private set; }
        public string AddressLine { get; private set; }
        public Point? Location { get; private set; }
        public int NeighborhoodId { get; private set; }
        public Neighborhood Neighborhood { get; private set; }
    }
}
