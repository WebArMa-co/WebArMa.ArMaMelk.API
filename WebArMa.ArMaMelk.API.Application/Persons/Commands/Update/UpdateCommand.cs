using Mediator;

namespace WebArMa.ArMaMelk.API.Application.Persons.Commands.Update
{
    public record UpdateCommand(Guid Guid, string FirstName, string LastName) : IRequest;
}
