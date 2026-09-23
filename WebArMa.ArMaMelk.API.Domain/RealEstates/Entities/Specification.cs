using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.RealEstates.Entities
{
    public sealed class Specification : EntityBase
    {
        public static Specification Create(decimal area, decimal? landArea = null, int? rooms = null, int? bedrooms = null, int? floor = null, int? totalFloors = null, int? unitCount = null, int? unitPerFloor = null, int? yearBuilt = null)
        {
            return new Specification
            {
                Area = area,
                LandArea = landArea,
                Rooms = rooms,
                Bedrooms = bedrooms,
                Floor = floor,
                TotalFloors = totalFloors,
                UnitCount = unitCount,
                UnitPerFloor = unitPerFloor,
                YearBuilt = yearBuilt
            };
        }
        public void Update(decimal area, decimal? landArea = null, int? rooms = null, int? bedrooms = null, int? floor = null, int? totalFloors = null, int? unitCount = null, int? unitPerFloor = null, int? yearBuilt = null)
        {
            Area = area;
            LandArea = landArea;
            Rooms = rooms;
            Bedrooms = bedrooms;
            Floor = floor;
            TotalFloors = totalFloors;
            UnitCount = unitCount;
            UnitPerFloor = unitPerFloor;
            YearBuilt = yearBuilt;
        }

        private Specification()
        {
        }

        public decimal Area { get; private set; }
        public decimal? LandArea { get; private set; }
        public int? Rooms { get; private set; }
        public int? Bedrooms { get; private set; }
        public int? Floor { get; private set; }
        public int? TotalFloors { get; private set; }
        public int? UnitCount { get; private set; }
        public int? UnitPerFloor { get; private set; }
        public int? YearBuilt { get; private set; }
    }
}
