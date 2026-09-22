using Mediator;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Commands.Create
{
    public record CreateCommand(string PhoneNumber, string Name, string FamilyName) : IRequest;
}
