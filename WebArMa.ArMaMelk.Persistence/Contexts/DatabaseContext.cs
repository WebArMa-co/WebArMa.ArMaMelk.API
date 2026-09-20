using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Helpers;
using WebArMa.ArMaMelk.API.Domain.Auth.Entities;
using WebArMa.ArMaMelk.API.Domain.Locations.Entities;
using WebArMa.ArMaMelk.API.Domain.OTPs;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;
using WebArMa.ArMaMelk.API.Domain.RealStates.Entities;
using WebArMa.ArMaMelk.API.Domain.Users.Entities;
using WebArMa.ArMaMelk.Persistence.Configurations;

namespace WebArMa.ArMaMelk.Persistence.Contexts
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options), IDatabaseContext
    {
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<OTP> OTPs { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<UserPerson> UserPersons { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Features> Features { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyOwnership> PropertyOwnerships { get; set; }
        public DbSet<PropertyShare> PropertyShares { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonConfigurations).Assembly);

            var converter = new ValueConverter<string, string>(v => PersianTextNormalizer.Normalize(v), v => v);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(string))
                    {
                        property.SetValueConverter(converter);
                    }
                }
            }
        }
    }
}
