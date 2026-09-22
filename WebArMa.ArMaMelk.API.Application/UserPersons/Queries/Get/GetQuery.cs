using Mediator;
using WebArMa.ArMaMelk.API.Application.UserPersons.DTOs;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Queries.Get
{
    public record GetQuery(int LastId, int Page, int PageSize, string? Search) : IRequest<IEnumerable<UserPersonDTO>>;
}
