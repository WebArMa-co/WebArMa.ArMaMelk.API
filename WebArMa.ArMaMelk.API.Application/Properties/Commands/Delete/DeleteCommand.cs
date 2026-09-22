using Mediator;

namespace WebArMa.ArMaMelk.API.Application.Properties.Commands.Delete
{
    public record DeleteCommand(Guid Guid) : IRequest;
}
