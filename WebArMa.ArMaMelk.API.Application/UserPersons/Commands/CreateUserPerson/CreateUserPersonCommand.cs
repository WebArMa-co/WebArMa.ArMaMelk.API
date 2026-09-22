using Mediator;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Commands.CreateUserPerson
{
    public record CreateUserPersonCommand(string PhoneNumber, string Name, string FamilyName) : IRequest;
}
