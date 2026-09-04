using FluentValidation;

namespace WebArMa.ArMaMelk.API.Application.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(x => x.Guid).NotEmpty().WithErrorCode("Guid-Required");
            RuleFor(x => x.LastName).NotEmpty().WithErrorCode("LastName-Required").MinimumLength(3).WithErrorCode("LastName-MinimumLength");
        }
    }
}
