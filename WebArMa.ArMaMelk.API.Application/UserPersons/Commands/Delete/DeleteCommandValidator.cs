using FluentValidation;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Commands.Delete
{
    public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(x => x.UserPersonGuid)
                .NotEmpty()
                .WithErrorCode("UserPersonGuid-Required");
        }
    }
}
