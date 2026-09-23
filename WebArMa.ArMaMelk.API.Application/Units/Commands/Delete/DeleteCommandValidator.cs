using FluentValidation;

namespace WebArMa.ArMaMelk.API.Application.Properties.Commands.Delete
{
    public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(x => x.Guid).NotEmpty().WithErrorCode("Required");
        }
    }
}
