using FluentValidation;

namespace WebArMa.ArMaMelk.API.Application.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithErrorCode("UserName-Required");
        }
    }
}
