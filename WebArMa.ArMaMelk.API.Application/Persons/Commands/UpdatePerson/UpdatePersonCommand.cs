using Mediator;

namespace WebArMa.ArMaMelk.API.Application.Persons.Commands.UpdatePerson
{
    public record UpdatePersonCommand(Guid Guid, string FirstName, string LastName) : IRequest;
}
