using Mediator;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Commands.Update
{
    public record UpdateCommand(Guid UserPersonGuid, string PhoneNumber, string Name, string FamilyName) : IRequest;
}
