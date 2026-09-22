using FluentValidation;

namespace WebArMa.ArMaMelk.API.Application.Persons.Commands.Create
{
    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("Required").MinimumLength(3).WithErrorCode("MinimumLength:3");
            RuleFor(x => x.FamilyName).NotEmpty().WithErrorCode("Required").MinimumLength(3).WithErrorCode("MinimumLength:3");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithErrorCode("Required").Matches(@"^09\d{9}$").WithErrorCode("Invalid");
        }
    }
}
