using Mediator;

namespace WebArMa.ArMaMelk.API.Application.Persons.Commands.CreatePerson
{
    public record CreatePersonCommand(string FirstName, string LastName, string PhoneNumber) : IRequest<Guid>;
}
