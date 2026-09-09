using WebArMa.ArMaMelk.API.Domain._Shared.Entities;
using WebArMa.ArMaMelk.API.Domain.RealStates.Enums;

namespace WebArMa.ArMaMelk.API.Domain.RealStates.Entities
{
    public class Building : EntityBase
    {
        public static Building Create(int? totalFloors = null, int? unitCount = null, int? unitPerFloor = null, ConstructionType? constructionType = null, FacadeType? facadeType = null)
        {
            return new Building
            {
                TotalFloors = totalFloors,
                UnitCount = unitCount,
                UnitPerFloor = unitPerFloor,
                ConstructionType = constructionType,
                FacadeType = facadeType
            };
        }
        public void Update(int? totalFloors = null, int? unitCount = null, int? unitPerFloor = null, ConstructionType? constructionType = null, FacadeType? facadeType = null)
        {
            TotalFloors = totalFloors;
            UnitCount = unitCount;
            UnitPerFloor = unitPerFloor;
            ConstructionType = constructionType;
            FacadeType = facadeType;
        }

        private Building()
        {
        }

        public int? TotalFloors { get; private set; }
        public int? UnitCount { get; private set; }
        public int? UnitPerFloor { get; private set; }
        public ConstructionType? ConstructionType { get; private set; }
        public FacadeType? FacadeType { get; private set; }
    }
}
