using System.Linq;
using ReiDoChopp.Domain.OrderAdditionalFees.Entities;
using ReiDoChopp.Domain.OrderAdditionalFees.Repositories.Filters;
using ReiDoChopp.Domain.Utils;

namespace ReiDoChopp.Domain.OrderAdditionalFees.Repositories
{
    public interface IOrderAdditionalFeesRepository : IEntityFrameworkRepository<OrderAdditionalFee>
    {
        IQueryable<OrderAdditionalFee>Filter(OrderAdditionalFeesListFilter filter);
    }
}
