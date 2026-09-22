using FluentValidation;

namespace WebArMa.ArMaMelk.API.Application.Persons.Commands.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(x => x.Guid).NotEmpty().WithErrorCode("Required");
            RuleFor(x => x.LastName).NotEmpty().WithErrorCode("Required").MinimumLength(3).WithErrorCode("MinimumLength");
        }
    }
}
