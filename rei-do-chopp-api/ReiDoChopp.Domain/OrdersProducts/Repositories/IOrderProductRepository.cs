using System.Linq;
using ReiDoChopp.Domain.OrdersProducts.Entities;
using ReiDoChopp.Domain.OrdersProducts.Repositories.Filters;
using ReiDoChopp.Domain.Utils;

namespace ReiDoChopp.Domain.OrdersProducts.Repositories
{
    public interface IOrdersProductsRepository : IEntityFrameworkRepository<OrderProduct>
    {
        IQueryable<OrderProduct>Filter(OrdersProductsListFilter filter);
    }
}
