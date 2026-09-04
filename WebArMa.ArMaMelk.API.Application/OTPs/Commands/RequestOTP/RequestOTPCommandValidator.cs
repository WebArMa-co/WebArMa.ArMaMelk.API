using FluentValidation;

namespace WebArMa.ArMaMelk.API.Application.OTPs.Commands.RequestOTP
{
    public class RequestOTPCommandValidator : AbstractValidator<RequestOTPCommand>
    {
        public RequestOTPCommandValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithErrorCode("PhoneNumber-Required").Matches(@"^09\d{9}$").WithErrorCode("PhoneNumber-Invalid");
        }
    }
}
