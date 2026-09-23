using Mediator;
using WebArMa.ArMaMelk.API.Domain.RealStates.Enums;

namespace WebArMa.ArMaMelk.API.Application.Properties.Commands.Create
{
    public record CreateCommand(
        string Title,
        PropertyType PropertyType,
        UsageType UsageType,
        int VillageId,
        string SystemAddress,
        string AddressLine,
        decimal Area,
        decimal? LandArea,
        int? Rooms,
        int? Bedrooms,
        int? Floor,
        int? TotalFloors,
        int? UnitCount,
        int? UnitPerFloor,
        int? YearBuilt,
        bool HasParking,
        int? ParkingCount,
        bool HasStorage,
        decimal? StorageArea,
        bool HasElevator,
        bool HasBalcony,
        bool HasTerrace,
        bool HasYard,
        bool HasPool,
        bool HasSauna,
        bool HasJacuzzi,
        bool HasSecurity,
        bool HasCCTV,
        Guid PersonGuid,
        PropertyDocumentType DocumentType,
        PropertyDocumentStatus DocumentStatus,
        PropertyOwnershipType OwnershipType,
        int? BuildingTotalFloors = null,
        int? BuildingUnitCount = null,
        int? BuildingUnitPerFloor = null,
        ConstructionType? ConstructionType = null,
        FacadeType? FacadeType = null) : IRequest<Guid>;
}
