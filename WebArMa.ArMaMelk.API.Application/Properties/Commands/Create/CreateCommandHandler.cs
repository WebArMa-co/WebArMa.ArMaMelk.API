using Mediator;
using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Domain.Locations.Entities;
using WebArMa.ArMaMelk.API.Domain.RealStates.Entities;

namespace WebArMa.ArMaMelk.API.Application.Properties.Commands.Create
{
    public class CreateCommandHandler(IDatabaseContext databaseContext) : IRequestHandler<CreateCommand, Guid>
    {
        public async ValueTask<Guid> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var village = await databaseContext.Villages.FirstOrDefaultAsync(v => v.Id == request.VillageId, cancellationToken) ?? throw new NotFoundException(nameof(Village));

            var person = await databaseContext.Persons.FirstOrDefaultAsync(p => p.Guid == request.PersonGuid, cancellationToken) ?? throw new NotFoundException("Person");

            var address = Address.Create(village, request.SystemAddress, request.AddressLine);
            var specifications = Specification.Create(
                request.Area,
                request.LandArea,
                request.Rooms,
                request.Bedrooms,
                request.Floor,
                request.TotalFloors,
                request.UnitCount,
                request.UnitPerFloor,
                request.YearBuilt);
            var features = Features.Create(
                request.HasParking,
                request.ParkingCount,
                request.HasStorage,
                request.StorageArea,
                request.HasElevator,
                request.HasBalcony,
                request.HasTerrace,
                request.HasYard,
                request.HasPool,
                request.HasSauna,
                request.HasJacuzzi,
                request.HasSecurity,
                request.HasCCTV);
            var ownership = PropertyOwnership.Create(
                person,
                request.DocumentType,
                request.DocumentStatus,
                request.OwnershipType);

            Building? building = null;
            if (request.BuildingTotalFloors.HasValue || request.BuildingUnitCount.HasValue || request.BuildingUnitPerFloor.HasValue ||
                request.ConstructionType.HasValue || request.FacadeType.HasValue)
            {
                building = Building.Create(
                    request.BuildingTotalFloors,
                    request.BuildingUnitCount,
                    request.BuildingUnitPerFloor,
                    request.ConstructionType,
                    request.FacadeType);
            }

            var property = Property.Create(
                request.Title,
                request.PropertyType,
                request.UsageType,
                address,
                specifications,
                features,
                ownership,
                building);

            await databaseContext.Properties.AddAsync(property, cancellationToken);
            await databaseContext.SaveChangesAsync(cancellationToken);

            return property.Guid;
        }
    }
}
