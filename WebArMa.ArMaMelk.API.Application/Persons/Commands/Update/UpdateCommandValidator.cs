using FluentValidation;

namespace WebArMa.ArMaMelk.API.Application.Persons.Commands.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(x => x.Guid).NotEmpty().WithErrorCode("Guid-Required");
            RuleFor(x => x.LastName).NotEmpty().WithErrorCode("LastName-Required").MinimumLength(3).WithErrorCode("LastName-MinimumLength");
        }
    }
}
