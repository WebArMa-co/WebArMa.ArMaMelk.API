using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.RealEstates.Entities
{
    public class Features : EntityBase
    {
        public static Features Create(bool hasParking, int? parkingCount, bool hasStorage, decimal? storageArea, bool hasElevator, bool hasBalcony, bool hasTerrace, bool hasYard, bool hasPool, bool hasSauna, bool hasJacuzzi, bool hasSecurity, bool hasCCTV)
        {
            return new Features
            {
                HasParking = hasParking,
                ParkingCount = parkingCount,
                HasStorage = hasStorage,
                StorageArea = storageArea,
                HasElevator = hasElevator,
                HasBalcony = hasBalcony,
                HasTerrace = hasTerrace,
                HasYard = hasYard,
                HasPool = hasPool,
                HasSauna = hasSauna,
                HasJacuzzi = hasJacuzzi,
                HasSecurity = hasSecurity,
                HasCCTV = hasCCTV
            };
        }
        public void Update(bool hasParking = false, int? parkingCount = null, bool hasStorage = false, decimal? storageArea = null, bool hasElevator = false, bool hasBalcony = false, bool hasTerrace = false, bool hasYard = false, bool hasPool = false, bool hasSauna = false, bool hasJacuzzi = false, bool hasSecurity = false, bool hasCCTV = false)
        {
            HasParking = hasParking;
            ParkingCount = parkingCount;
            HasStorage = hasStorage;
            StorageArea = storageArea;
            HasElevator = hasElevator;
            HasBalcony = hasBalcony;
            HasTerrace = hasTerrace;
            HasYard = hasYard;
            HasPool = hasPool;
            HasSauna = hasSauna;
            HasJacuzzi = hasJacuzzi;
            HasSecurity = hasSecurity;
            HasCCTV = hasCCTV;
        }

        private Features()
        {
        }

        public bool HasParking { get; private set; }
        public int? ParkingCount { get; private set; }
        public bool HasStorage { get; private set; }
        public decimal? StorageArea { get; private set; }
        public bool HasElevator { get; private set; }
        public bool HasBalcony { get; private set; }
        public bool HasTerrace { get; private set; }
        public bool HasYard { get; private set; }
        public bool HasPool { get; private set; }
        public bool HasSauna { get; private set; }
        public bool HasJacuzzi { get; private set; }
        public bool HasSecurity { get; private set; }
        public bool HasCCTV { get; private set; }
    }
}
