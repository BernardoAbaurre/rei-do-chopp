using ReiDoChopp.Domain.Orders.Repositories.Models;
using ReiDoChopp.Domain.OrdersProducts.Entities;
using ReiDoChopp.Domain.OrdersProducts.Repositories;
using ReiDoChopp.Domain.OrdersProducts.Repositories.Filters;
using ReiDoChopp.Infra.Data;
using ReiDoChopp.Infra.Utils;

namespace ReiDoChopp.Infra.OrdersProducts.Repositories
{
    public class OrdersProductsRepository : EntityFrameworkRepository<OrderProduct>, IOrdersProductsRepository
    {
        public OrdersProductsRepository(ReiDoChoppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<OrderProduct> Filter(OrdersProductsListFilter filter)
        {
            IQueryable<OrderProduct> query = Query();

            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (filter.OrderId.HasValue)
            {
                query = query.Where(x => x.Order.Id == filter.OrderId.Value);
            }
            if (filter.ProductId.HasValue)
            {
                query = query.Where(x => x.Product.Id == filter.ProductId.Value);
            }
            if (filter.Quantity.HasValue)
            {
                query = query.Where(x => x.Quantity == filter.Quantity.Value);
            }
            if (filter.UnitPriceCharged.HasValue)
            {
                query = query.Where(x => x.UnitPriceCharged == filter.UnitPriceCharged.Value);
            }

            return query;
        }
    }
}
