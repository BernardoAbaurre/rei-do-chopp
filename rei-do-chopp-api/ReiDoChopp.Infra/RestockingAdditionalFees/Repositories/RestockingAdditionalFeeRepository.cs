using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReiDoChopp.Domain.RestockingAdditionalFees.Entities;
using ReiDoChopp.Domain.RestockingAdditionalFees.Repositories;
using ReiDoChopp.Domain.RestockingAdditionalFees.Repositories.Filters;
using ReiDoChopp.Infra.Utils;
using ReiDoChopp.Infra.Data;

namespace ReiDoChopp.Infra.RestockingAdditionalFees.Repositories
{
    public class RestockingAdditionalFeesRepository : EntityFrameworkRepository<RestockingAdditionalFee>, IRestockingAdditionalFeesRepository
    {
        public RestockingAdditionalFeesRepository(ReiDoChoppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<RestockingAdditionalFee> Filter(RestockingAdditionalFeesListFilter filter)
        {
            var query = Query();
            
            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (filter.RestockingId.HasValue)
            {
                query = query.Where(x => x.Restocking.Id == filter.RestockingId.Value);
            }
            if (filter.Value.HasValue)
            {
                query = query.Where(x => x.Value == filter.Value.Value);
            }
            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(x => x.Description.ToUpper().Contains(filter.Description.ToUpper()));
            }

            return query;
        }
    }
}
