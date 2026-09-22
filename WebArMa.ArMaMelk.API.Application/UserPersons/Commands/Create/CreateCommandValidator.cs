using FluentValidation;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Commands.Create
{
    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithErrorCode("PhoneNumber-Required")
                .Matches(@"^09\d{9}$")
                .WithErrorCode("PhoneNumber-Invalid");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode("Name-Required")
                .MinimumLength(3)
                .WithErrorCode("Name-MinimumLength");

            RuleFor(x => x.FamilyName)
                .NotEmpty()
                .WithErrorCode("FamilyName-Required")
                .MinimumLength(3)
                .WithErrorCode("FamilyName-MinimumLength");
        }
    }
}
