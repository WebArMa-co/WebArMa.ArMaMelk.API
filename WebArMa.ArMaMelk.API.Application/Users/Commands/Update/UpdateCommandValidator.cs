using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace WebArMa.ArMaMelk.API.Application.Users.Commands.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        private static readonly Regex PersianNameRegex = new(@"^[\u0621-\u063A\u0641-\u0648\u067E\u0686\u0698\u06AF\u06CC ]+$");
        public UpdateCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("Required").Matches(PersianNameRegex).WithErrorCode("Invalid").MaximumLength(100).WithErrorCode("MaxLength:100");
            RuleFor(x => x.FamilyName).NotEmpty().WithErrorCode("Required").Matches(PersianNameRegex).WithErrorCode("Invalid").MaximumLength(100).WithErrorCode("MaxLength:100");
            RuleFor(x => x.DisplayName).MaximumLength(100).WithErrorCode("MaxLength:100");
            RuleFor(x => x.PhotoURL).MaximumLength(500).WithMessage("Invalid:FileName").Must(BeValidUrl).WithMessage("Invalid:PhotoUrl");
        }

        private static bool BeValidUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return true;
            }

            return Uri.TryCreate(url, UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
