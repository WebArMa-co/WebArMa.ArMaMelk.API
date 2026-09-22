using Mediator;

namespace WebArMa.ArMaMelk.API.Application.Persons.Commands.Create
{
    public record CreateCommand(string Name, string FamilyName, string PhoneNumber) : IRequest<Guid>;
}
