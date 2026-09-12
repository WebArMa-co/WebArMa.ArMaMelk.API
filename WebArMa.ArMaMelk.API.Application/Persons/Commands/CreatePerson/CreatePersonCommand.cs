using Mediator;

namespace WebArMa.ArMaMelk.API.Application.Persons.Commands.CreatePerson
{
    public record CreatePersonCommand(string Name, string FamilyName, string PhoneNumber) : IRequest<Guid>;
}
