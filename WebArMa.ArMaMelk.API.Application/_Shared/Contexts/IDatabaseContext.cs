using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Domain.Auth.Entities;
using WebArMa.ArMaMelk.API.Domain.OTPs;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;

namespace WebArMa.ArMaMelk.API.Application._Shared.Contexts
{
    public interface IDatabaseContext
    {
        DbSet<Person> Persons { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<OTP> OTPs { get; set; }
        DbSet<Token> Tokens { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
