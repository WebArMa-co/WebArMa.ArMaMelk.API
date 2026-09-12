using Mediator;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Security.Cryptography;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application._Shared.Helpers;
using WebArMa.ArMaMelk.API.Domain.OTPs;

namespace WebArMa.ArMaMelk.API.Application.OTPs.Commands.Request
{
    public class RequestCommandHandler(IConfiguration configuration, IDatabaseContext databaseContext) : IRequestHandler<RequestCommand>
    {
        public async ValueTask<Unit> Handle(RequestCommand request, CancellationToken cancellationToken)
        {
            var otp = RandomNumberGenerator.GetInt32(10000, 100000).ToString();
            Debugger.Break();
            var secret = configuration["Otp:Secret"] ?? throw new InvalidOperationException("OTP secret is not configured.");
            var expireTime = Convert.ToInt32(configuration["Otp:Expire"]);
            var hashedOTP = Hasher.Hash(otp, secret);
            await databaseContext.OTPs.AddAsync(OTP.Create(request.PhoneNumber, hashedOTP, DateTimeOffset.UtcNow.AddMinutes(expireTime)), cancellationToken);
            await databaseContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
