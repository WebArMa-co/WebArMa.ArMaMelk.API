using Mediator;

namespace WebArMa.ArMaMelk.API.Application.OTPs.Commands.Request
{
    public record RequestCommand(string PhoneNumber) : IRequest;
}
