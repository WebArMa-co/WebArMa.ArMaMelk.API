using Mediator;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Commands.UpdateUserPerson
{
    public record UpdateUserPersonCommand(Guid UserPersonGuid, string PhoneNumber, string Name, string FamilyName) : IRequest;
}
