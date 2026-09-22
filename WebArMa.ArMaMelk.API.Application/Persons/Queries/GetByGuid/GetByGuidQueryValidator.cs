using FluentValidation;

namespace WebArMa.ArMaMelk.API.Application.Persons.Queries.GetByGuid
{
    public class GetByGuidQueryValidator : AbstractValidator<GetByGuidQuery>
    {
        public GetByGuidQueryValidator()
        {
            RuleFor(x => x.Guid)
                .NotEmpty()
                .WithErrorCode("Guid-Required");
        }
    }
}
