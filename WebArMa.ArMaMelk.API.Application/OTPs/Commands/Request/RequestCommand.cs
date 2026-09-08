using Mediator;

namespace WebArMa.ArMaMelk.API.Application.OTPs.Commands.RequestOTP
{
    public record RequestCommand(string PhoneNumber) : IRequest;
}
