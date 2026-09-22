using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Application._Shared.Helpers;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Commands.CreateUserPerson
{
    public class CreateUserPersonValidation : AbstractValidator<CreateUserPersonCommand>
    {
        public CreateUserPersonValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("Name-Required").Matches(PersianNameRegex).WithErrorCode("Name-Invalid-PersianNameRegex").MaximumLength(100).WithErrorCode("Name-MaxLength-100");
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("Name-Required").Matches(PersianNameRegex).WithErrorCode("Name-Invalid-PersianNameRegex").MaximumLength(100).WithErrorCode("Name-MaxLength-100");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithErrorCode("PhoneNumber-Required").Must(IranianPhoneNumber.IsValidPhoneNumber).WithErrorCode("PhoneNumber-Invalid-IsValidPhoneNumber");
        }
    }
}
