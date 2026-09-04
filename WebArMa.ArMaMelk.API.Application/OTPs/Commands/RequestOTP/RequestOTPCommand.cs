using Mediator;

namespace WebArMa.ArMaMelk.API.Application.OTPs.Commands.RequestOTP
{
    public record RequestOTPCommand(string PhoneNumber) : IRequest;
}
