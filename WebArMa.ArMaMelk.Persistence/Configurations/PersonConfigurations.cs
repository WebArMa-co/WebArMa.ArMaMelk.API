using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;

namespace WebArMa.ArMaMelk.Persistence.Configurations
{
    public class PersonConfigurations : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {

        }
    }
}
