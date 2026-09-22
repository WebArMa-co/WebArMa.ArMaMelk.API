using FluentValidation;
using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application.UserPersons.DTOs;

namespace WebArMa.ArMaMelk.API.Application.UserPersons.Queries.Get
{
    public class GetQueryHandler(IDatabaseContext databaseContext, TypeAdapterConfig config) : IRequestHandler<GetQuery, IEnumerable<UserPersonDTO>>
    {
        public async ValueTask<IEnumerable<UserPersonDTO>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            var query = databaseContext.UserPersons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                query = query.Where(u => u.Name.Contains(search) || u.FamilyName.Contains(search) || u.Person.PhoneNumber.Contains(search) || (u.Name + " " + u.FamilyName).Contains(search));
            }

            query = query.Where(u => u.Id > request.LastId);

            return await query.OrderBy(u => u.Id).Take(request.PageSize).ProjectToType<UserPersonDTO>(config).ToListAsync(cancellationToken);

        }
    }
}
