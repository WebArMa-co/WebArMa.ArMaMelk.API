using Mediator;
using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Domain.Locations.Entities;
using WebArMa.ArMaMelk.API.Domain.RealStates.Entities;

namespace WebArMa.ArMaMelk.API.Application.Properties.Commands.Update
{
    public class UpdateCommandHandler(IDatabaseContext databaseContext) : IRequestHandler<UpdateCommand>
    {
        public async ValueTask<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var property = await databaseContext.Properties.Include(p => p.Address).Include(p => p.Specifications).Include(p => p.Features).Include(p => p.PropertyOwnership).Include(p => p.Building).FirstOrDefaultAsync(p => p.Guid == request.Guid, cancellationToken) ?? throw new NotFoundException(nameof(Property));

            var village = await databaseContext.Villages.FirstOrDefaultAsync(v => v.Id == request.VillageId, cancellationToken) ?? throw new NotFoundException(nameof(Village));

            var person = await databaseContext.Persons.FirstOrDefaultAsync(p => p.Guid == request.PersonGuid, cancellationToken) ?? throw new NotFoundException("Person");

            property.Address.Update(village, request.SystemAddress, request.AddressLine);

            property.Specifications.Update(
                request.Area,
                request.LandArea,
                request.Rooms,
                request.Bedrooms,
                request.Floor,
                request.TotalFloors,
                request.UnitCount,
                request.UnitPerFloor,
                request.YearBuilt);

            property.Features.Update(
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

            property.PropertyOwnership.Update(
                person,
                request.DocumentType,
                request.DocumentStatus,
                request.OwnershipType);

            var hasBuilding = request.BuildingTotalFloors.HasValue || request.BuildingUnitCount.HasValue || request.BuildingUnitPerFloor.HasValue || request.ConstructionType.HasValue || request.FacadeType.HasValue;

            if (hasBuilding)
            {
                if (property.Building == null)
                {
                    property.Update(
                        request.Title,
                        request.PropertyType,
                        request.UsageType,
                        property.Address,
                        property.Specifications,
                        property.Features,
                        property.PropertyOwnership,
                        Building.Create(
                            request.BuildingTotalFloors,
                            request.BuildingUnitCount,
                            request.BuildingUnitPerFloor,
                            request.ConstructionType,
                            request.FacadeType));
                }
                else
                {
                    property.Building.Update(
                        request.BuildingTotalFloors,
                        request.BuildingUnitCount,
                        request.BuildingUnitPerFloor,
                        request.ConstructionType,
                        request.FacadeType);
                }
            }
            else if (property.Building != null)
            {
                databaseContext.Buildings.Remove(property.Building);
                property.Update(
                    request.Title,
                    request.PropertyType,
                    request.UsageType,
                    property.Address,
                    property.Specifications,
                    property.Features,
                    property.PropertyOwnership);
            }
            else
            {
                property.Update(
                    request.Title,
                    request.PropertyType,
                    request.UsageType,
                    property.Address,
                    property.Specifications,
                    property.Features,
                    property.PropertyOwnership);
            }

            await databaseContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
