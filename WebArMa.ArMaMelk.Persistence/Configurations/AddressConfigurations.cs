using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebArMa.ArMaMelk.API.Domain.RealStates.Entities;

namespace WebArMa.ArMaMelk.Persistence.Configurations
{
    public class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.Location).HasColumnType("geometry (point)");
        }
    }
}
