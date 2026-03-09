using ReiDoChopp.Domain.OrderAdditionalFees.Entities;
using ReiDoChopp.Domain.OrderAdditionalFees.Repositories;
using ReiDoChopp.Domain.OrderAdditionalFees.Repositories.Filters;
using ReiDoChopp.Infra.Data;
using ReiDoChopp.Infra.Utils;

namespace ReiDoChopp.Infra.OrderAdditionalFees.Repositories
{
    public class OrderAdditionalFeesRepository : EntityFrameworkRepository<OrderAdditionalFee>, IOrderAdditionalFeesRepository
    {
        public OrderAdditionalFeesRepository(ReiDoChoppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<OrderAdditionalFee> Filter(OrderAdditionalFeesListFilter filter)
        {
            IQueryable<OrderAdditionalFee> query = Query();

            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (filter.OrderId.HasValue)
            {
                query = query.Where(x => x.Order.Id == filter.OrderId.Value);
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
