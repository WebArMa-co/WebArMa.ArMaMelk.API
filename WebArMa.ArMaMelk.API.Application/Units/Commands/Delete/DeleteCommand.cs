using Mediator;

namespace WebArMa.ArMaMelk.API.Application.Units.Commands.Delete
{
    public record DeleteCommand(Guid Guid) : IRequest;
}
