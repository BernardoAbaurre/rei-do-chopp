using System.Linq;
using ReiDoChopp.Domain.Orders.Entities;
using ReiDoChopp.Domain.Orders.Repositories.Filters;
using ReiDoChopp.Domain.Orders.Repositories.Models;
using ReiDoChopp.Domain.Orders.Services.Commands;
using ReiDoChopp.Domain.OrdersProducts.Repositories.Filters;
using ReiDoChopp.Domain.Utils;
using ReiDoChopp.Domain.Utils.Models;

namespace ReiDoChopp.Domain.Orders.Repositories
{
    public interface IOrdersRepository : IEntityFrameworkRepository<Order>
    {
        IQueryable<Order>Filter(OrdersListFilter filter);
        Task<PaginationModel<OrderHistoryModel>> GetOrderHistory(IQueryable<Order> query, OrdersListFilter filter);
        OrderTotalsCalculationModel CalculateTotals(OrderCommand command);
    }
}
