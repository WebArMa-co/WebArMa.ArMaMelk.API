using Mediator;
using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Domain.Locations.Entities;
using WebArMa.ArMaMelk.API.Domain.RealEstates.Entities;

namespace WebArMa.ArMaMelk.API.Application.Units.Commands.Update
{
    public class UpdateCommandHandler(IDatabaseContext databaseContext) : IRequestHandler<UpdateCommand>
    {
        public async ValueTask<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var realEstate = await databaseContext.RealEstates.Include(p => p.Address).Include(p => p.Specifications).Include(p => p.Features).Include(p => p.RealEstateOwnership).Include(p => p.Building).FirstOrDefaultAsync(p => p.Guid == request.Guid, cancellationToken) ?? throw new NotFoundException(nameof(RealEstate));

            var village = await databaseContext.Villages.FirstOrDefaultAsync(v => v.Id == request.VillageId, cancellationToken) ?? throw new NotFoundException(nameof(Village));

            var person = await databaseContext.Persons.FirstOrDefaultAsync(p => p.Guid == request.PersonGuid, cancellationToken) ?? throw new NotFoundException("Person");

            realEstate.Address.Update(village, request.SystemAddress, request.AddressLine);

            realEstate.Specifications.Update(
                request.Area,
                request.LandArea,
                request.Rooms,
                request.Bedrooms,
                request.Floor,
                request.TotalFloors,
                request.RealEstateCount,
                request.RealEstatePerFloor,
                request.YearBuilt);

            realEstate.Features.Update(
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

            realEstate.RealEstateOwnership.Update(
                person,
                request.DocumentType,
                request.DocumentStatus,
                request.OwnershipType);

            var hasBuilding = request.BuildingTotalFloors.HasValue || request.BuildingRealEstateCount.HasValue || request.BuildingRealEstatePerFloor.HasValue || request.ConstructionType.HasValue || request.FacadeType.HasValue;

            if (hasBuilding)
            {
                if (realEstate.Building == null)
                {
                    realEstate.Update(
                        request.Title,
                        request.RealEstateType,
                        request.UsageType,
                        realEstate.Address,
                        realEstate.Specifications,
                        realEstate.Features,
                        realEstate.RealEstateOwnership,
                        Building.Create(
                            request.BuildingTotalFloors,
                            request.BuildingRealEstateCount,
                            request.BuildingRealEstatePerFloor,
                            request.ConstructionType,
                            request.FacadeType));
                }
                else
                {
                    realEstate.Building.Update(
                        request.BuildingTotalFloors,
                        request.BuildingRealEstateCount,
                        request.BuildingRealEstatePerFloor,
                        request.ConstructionType,
                        request.FacadeType);
                }
            }
            else if (realEstate.Building != null)
            {
                databaseContext.Buildings.Remove(realEstate.Building);
                realEstate.Update(
                    request.Title,
                    request.RealEstateType,
                    request.UsageType,
                    realEstate.Address,
                    realEstate.Specifications,
                    realEstate.Features,
                    realEstate.RealEstateOwnership);
            }
            else
            {
                realEstate.Update(
                    request.Title,
                    request.RealEstateType,
                    request.UsageType,
                    realEstate.Address,
                    realEstate.Specifications,
                    realEstate.Features,
                    realEstate.RealEstateOwnership);
            }

            await databaseContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
