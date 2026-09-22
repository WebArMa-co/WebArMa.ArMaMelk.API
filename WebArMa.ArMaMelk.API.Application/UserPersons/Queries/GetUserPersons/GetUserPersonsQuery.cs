using Mediator;
using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application.UserPersons.DTOs;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Queries.GetUserPersons
{
    public record GetUserPersonsQuery(int LastId, int Page, int PageSize, string Search) : IRequest<IEnumerable<UserPersonDTO>>;
    public class GetUserPersonsQueryHandler(IDatabaseContext databaseContext) : IRequestHandler<GetUserPersonsQuery, IEnumerable<UserPersonDTO>>
    {
        public ValueTask<IEnumerable<UserPersonDTO>> Handle(GetUserPersonsQuery request, CancellationToken cancellationToken)
        {
            var query = databaseContext.UserPersons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                query = query.Where(u => u.Name.Contains(search) || u.FamilyName.Contains(search) || u.Person.PhoneNumber.Contains(search) || (u.Name + " " + u.FamilyName).Contains(search));
            }

            query = query.Where(u => u.Id > request.LastId);

            return await query.OrderBy(u => u.Id).Take(request.PageSize).Select(u => new UserPersonDTO
                {
                    Name = u.Name,
                    FamilyName = u.FamilyName,
                    PhoneNumber = u.Person.PhoneNumber
                }).ToListAsync(cancellationToken);
        }
    }
}
