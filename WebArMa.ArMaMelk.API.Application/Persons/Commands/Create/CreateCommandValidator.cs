using FluentValidation;

namespace WebArMa.ArMaMelk.API.Application.Persons.Commands.Create
{
    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("FirstName-Required").MinimumLength(3).WithErrorCode("FirstName-MinimumLength");
            RuleFor(x => x.FamilyName).NotEmpty().WithErrorCode("LastName-Required").MinimumLength(3).WithErrorCode("LastName-MinimumLength");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithErrorCode("PhoneNumber-Required").Matches(@"^09\d{9}$").WithErrorCode("PhoneNumber-Invalid");
        }
    }
}
