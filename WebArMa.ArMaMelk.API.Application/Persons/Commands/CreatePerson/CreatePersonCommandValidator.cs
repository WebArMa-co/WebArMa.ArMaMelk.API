using FluentValidation;

namespace WebArMa.ArMaMelk.API.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithErrorCode("FirstName-Required").MinimumLength(3).WithErrorCode("FirstName-MinimumLength");
            RuleFor(x => x.LastName).NotEmpty().WithErrorCode("LastName-Required").MinimumLength(3).WithErrorCode("LastName-MinimumLength");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithErrorCode("PhoneNumber-Required").Matches(@"^09\d{9}$").WithErrorCode("PhoneNumber-Invalid");
        }
    }
}
