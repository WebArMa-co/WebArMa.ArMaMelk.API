using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebArMa.ArMaMelk.API.Application._Shared.Helpers;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Commands.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        private static readonly Regex PersianNameRegex = new(@"^[\u0621-\u063A\u0641-\u0648\u067E\u0686\u0698\u06AF\u06CC ]+$");
        public UpdateCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("Name-Required").Matches(PersianNameRegex).WithErrorCode("Name-Invalid-PersianNameRegex").MaximumLength(100).WithErrorCode("Name-MaxLength-100");
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("Name-Required").Matches(PersianNameRegex).WithErrorCode("Name-Invalid-PersianNameRegex").MaximumLength(100).WithErrorCode("Name-MaxLength-100");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithErrorCode("PhoneNumber-Required").Must(IranianPhoneNumber.IsValidPhoneNumber).WithErrorCode("PhoneNumber-Invalid-IsValidPhoneNumber");
        }
    }
}
