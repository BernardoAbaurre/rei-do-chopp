using System.Linq;
using ReiDoChopp.Domain.Orders.Repositories.Models;
using ReiDoChopp.Domain.Restockings.Entities;
using ReiDoChopp.Domain.Restockings.Repositories.Filters;
using ReiDoChopp.Domain.Restockings.Repositories.Models;
using ReiDoChopp.Domain.Restockings.Services.Commands;
using ReiDoChopp.Domain.Utils;
using ReiDoChopp.Domain.Utils.Models;

namespace ReiDoChopp.Domain.Restockings.Repositories
{
    public interface IRestockingsRepository : IEntityFrameworkRepository<Restocking>
    {
        IQueryable<Restocking>Filter(RestockingsListFilter filter);
        Task<PaginationModel<RestockingHistoryModel>> GetRestockingHistory(IQueryable<Restocking> query, RestockingsListFilter filter);
        RestockingTotalsCalculationModel CalculateTotals(RestockingCommand command);
    }
}
