using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Exceptions;
using WebArMa.ArMaMelk.API.Application._Shared.Helpers;
using WebArMa.ArMaMelk.API.Application.Auth.DTOs;
using WebArMa.ArMaMelk.API.Application.Auth.Services;
using WebArMa.ArMaMelk.API.Domain.OTPs;
using WebArMa.ArMaMelk.API.Domain.Persons.Entities;
using WebArMa.ArMaMelk.API.Domain.Users.Entities;

namespace WebArMa.ArMaMelk.API.Application.Auth.Commands.Login
{
    public class LoginCommandHandler(IConfiguration configuration, IDatabaseContext databaseContext, ITokenService generateTokenService) : IRequestHandler<LoginCommand, TokenDTO>
    {
        public async ValueTask<TokenDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var secret = configuration["Otp:Secret"] ?? throw new ConfigurationException("Otp:Secret");
            var maxAttemptCount = Convert.ToInt32(configuration["Otp:MaxAttemptCount"]);
            var hashedOTP = Hasher.Hash(request.Code, secret);
            var otp = await databaseContext.OTPs.OrderByDescending(o => o.CreatedAt).FirstOrDefaultAsync(o => o.UsedAt == null && o.ExpiresAt > DateTimeOffset.UtcNow && o.AttemptCount < maxAttemptCount && o.UserName == request.UserName, cancellationToken) ?? throw new NotFoundException(nameof(OTP));

            otp.IncreaseAttempt();

            if (otp.CodeHash != hashedOTP)
            {
                await databaseContext.SaveChangesAsync(cancellationToken);
                throw new InvalidArgumentException(nameof(OTP.CodeHash));
            }

            otp.MarkAsUsed();
            await databaseContext.SaveChangesAsync(cancellationToken);

            var person = await databaseContext.Persons.FirstOrDefaultAsync(u => u.PhoneNumber == request.UserName, cancellationToken);

            if (person == null)
            {
                person = Person.Create(null, null, request.UserName);
                await databaseContext.Persons.AddAsync(person, cancellationToken);
                var newUser = User.Create(request.UserName, person);
                await databaseContext.Users.AddAsync(newUser, cancellationToken);
                await databaseContext.SaveChangesAsync(cancellationToken);
            }

            var user = await databaseContext.Users.FirstAsync(u => u.UserName == request.UserName, cancellationToken);

            return await generateTokenService.GenerateAsync(user.Id, Guid.Empty, "", cancellationToken);
        }
    }
}
