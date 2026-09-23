using Microsoft.EntityFrameworkCore;
using System.Xml;
using WebArMa.ArMaMelk.API.Domain.Auth.Entities;
using WebArMa.ArMaMelk.API.Domain.Locations.Entities;
using WebArMa.ArMaMelk.API.Domain.OTPs;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;
using WebArMa.ArMaMelk.API.Domain.RealEstates.Entities;
using WebArMa.ArMaMelk.API.Domain.Users.Entities;

namespace WebArMa.ArMaMelk.API.Application._Shared.Contexts
{
    public interface IDatabaseContext
    {
        DbSet<Token> Tokens { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<County> Counties { get; set; }
        DbSet<Province> Provinces { get; set; }
        DbSet<Village> Villages { get; set; }
        DbSet<OTP> OTPs { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<UserPerson> UserPersons { get; set; }
        DbSet<Building> Buildings { get; set; }
        DbSet<Features> Features { get; set; }
        DbSet<RealEstate> RealEstates { get; set; }
        DbSet<RealEstateOwnership> RealEstateOwnerships { get; set; }
        DbSet<RealEstateShare> RealEstateShares { get; set; }
        DbSet<Specification> Specifications { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
