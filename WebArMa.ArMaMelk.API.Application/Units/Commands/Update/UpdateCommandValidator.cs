using FluentValidation;

namespace WebArMa.ArMaMelk.API.Application.Units.Commands.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(x => x.Guid).NotEmpty().WithErrorCode("Required");
            RuleFor(x => x.Title).NotEmpty().WithErrorCode("Required");
            RuleFor(x => x.VillageId).GreaterThan(0).WithErrorCode("Required");
            RuleFor(x => x.SystemAddress).NotEmpty().WithErrorCode("Required");
            RuleFor(x => x.AddressLine).NotEmpty().WithErrorCode("Required");
            RuleFor(x => x.Area).GreaterThan(0).WithErrorCode("Required");
            RuleFor(x => x.PersonGuid).NotEmpty().WithErrorCode("Required");
        }
    }
}
