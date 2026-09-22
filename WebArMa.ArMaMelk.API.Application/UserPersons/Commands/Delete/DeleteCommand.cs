using Mediator;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Commands.Delete
{
    public record DeleteCommand(Guid UserPersonGuid) : IRequest;
}

