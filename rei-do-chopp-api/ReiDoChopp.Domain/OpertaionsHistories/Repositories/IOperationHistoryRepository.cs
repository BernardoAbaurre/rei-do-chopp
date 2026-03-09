
using ReiDoChopp.Domain.OpertaionsHistories.Repositories.Filters;
using ReiDoChopp.Domain.OpertaionsHistories.Repositories.Models;
using ReiDoChopp.Domain.Orders.Entities;
using ReiDoChopp.Domain.Restockings.Entities;

namespace ReiDoChopp.Domain.OpertaionsHistories.Repositories
{
    public interface IOpertaionsHistoriesRepository
    {
        Task<OperationHistoryModel> ListAsync(IQueryable<Order> ordersQuery, IQueryable<Restocking> restockingsQuery);
        IQueryable<Restocking> FilterRestocking(OpertaionsHistoriesListFilter filter);
        IQueryable<Order> FilterOrder(OpertaionsHistoriesListFilter filter);
    }
}
