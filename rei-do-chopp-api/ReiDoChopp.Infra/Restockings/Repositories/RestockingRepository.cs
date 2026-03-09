using ReiDoChopp.Domain.Orders.Repositories.Models;
using ReiDoChopp.Domain.Restockings.Entities;
using ReiDoChopp.Domain.Restockings.Repositories;
using ReiDoChopp.Domain.Restockings.Repositories.Filters;
using ReiDoChopp.Domain.Restockings.Repositories.Models;
using ReiDoChopp.Domain.Restockings.Services.Commands;
using ReiDoChopp.Domain.Utils.Extensions;
using ReiDoChopp.Domain.Utils.Models;
using ReiDoChopp.Infra.Data;
using ReiDoChopp.Infra.Utils;

namespace ReiDoChopp.Infra.Restockings.Repositories
{
    public class RestockingsRepository : EntityFrameworkRepository<Restocking>, IRestockingsRepository
    {
        public RestockingsRepository(ReiDoChoppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Restocking> Filter(RestockingsListFilter filter)
        {
            IQueryable<Restocking> query = Query();

            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (filter.InitialDate.HasValue)
            {
                query = query.Where(x => x.Date.Date >= filter.InitialDate.Value.Date);
            }
            if (filter.FinalDate.HasValue)
            {
                query = query.Where(x => x.Date.Date <= filter.FinalDate.Value.Date);
            }
            if (filter.UserId.HasValue)
            {
                query = query.Where(x => x.User.Id == filter.UserId.Value);
            }
            if (filter.ProductsIds != null && filter.ProductsIds.Length > 0)
            {
                query = query.Where(o => o.RestockingProducts.Any(op => filter.ProductsIds.Contains(op.ProductId)));
            }

            return query;
        }

        public async Task<PaginationModel<RestockingHistoryModel>> GetRestockingHistory(IQueryable<Restocking> query, RestockingsListFilter filter)
        {
            return await query.Select(r => new RestockingHistoryModel
            {
                Id = r.Id,
                UserName = r.User.FirstName + " " + r.User.LastName,
                Date = r.Date,
                ProductsSumValue = r.RestockingProducts.Sum(rp => rp.TotalPricePaid),
                Discount = r.Discount ?? 0,
                TotalFees = r.RestockingAdditionalFees.Sum(af => af.Value),
                TotalValue = r.RestockingProducts.Sum(rp => rp.TotalPricePaid) * (1 - ((r.Discount ?? 0) / 100)) + r.RestockingAdditionalFees.Sum(af => af.Value)
            }).PageAsync(filter);
        }

        public RestockingTotalsCalculationModel CalculateTotals(RestockingCommand command)
        {
            double totalProducts = command.RestockingProducts?.Sum(op => op.UnitPricePaid * op.Quantity) ?? 0;
            double totalFees = command.RestockingAdditionalFees?.Sum(af => af.Value) ?? 0;
            double fullValue = totalProducts + totalFees;

            return new RestockingTotalsCalculationModel
            {
                TotalProducts = totalProducts,
                TotalFees = totalFees,
                FullValue = fullValue,
                Total = totalProducts * (1 - ((command.Discount ?? 0) / 100)) + totalFees
            };
        }
    }
}
