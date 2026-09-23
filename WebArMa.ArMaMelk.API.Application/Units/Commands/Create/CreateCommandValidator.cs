using FluentValidation;

namespace WebArMa.ArMaMelk.API.Application.Units.Commands.Create
{
    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithErrorCode("Required");
            RuleFor(x => x.VillageId).GreaterThan(0).WithErrorCode("Required");
            RuleFor(x => x.SystemAddress).NotEmpty().WithErrorCode("Required");
            RuleFor(x => x.AddressLine).NotEmpty().WithErrorCode("Required");
            RuleFor(x => x.Area).GreaterThan(0).WithErrorCode("Required");
            RuleFor(x => x.PersonGuid).NotEmpty().WithErrorCode("Required");
        }
    }
}
