using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Queries.Get
{
    public class GetQueryValidator : AbstractValidator<GetQuery>
    {
        public GetQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0).WithErrorCode("GreaterThan:0");
            RuleFor(x => x.PageSize).GreaterThan(0).WithErrorCode("GreaterThan:0");
        }
    }
}
