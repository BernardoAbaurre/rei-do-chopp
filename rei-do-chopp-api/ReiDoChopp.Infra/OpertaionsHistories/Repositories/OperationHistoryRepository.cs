using Microsoft.EntityFrameworkCore;
using ReiDoChopp.Domain.OpertaionsHistories.Repositories;
using ReiDoChopp.Domain.OpertaionsHistories.Repositories.Filters;
using ReiDoChopp.Domain.OpertaionsHistories.Repositories.Models;
using ReiDoChopp.Domain.Orders.Entities;
using ReiDoChopp.Domain.Orders.Repositories;
using ReiDoChopp.Domain.Restockings.Entities;
using ReiDoChopp.Domain.Restockings.Repositories;
using ReiDoChopp.Infra.Data;

namespace ReiDoChopp.Infra.OpertaionsHistories.Repositories
{
    public class OpertaionsHistoriesRepository : IOpertaionsHistoriesRepository
    {
        private readonly ReiDoChoppDbContext dbContext;
        private readonly IOrdersRepository ordersRepository;
        private readonly IRestockingsRepository restockingsRepository;

        public OpertaionsHistoriesRepository(
            ReiDoChoppDbContext dbContext,
            IOrdersRepository ordersRepository,
            IRestockingsRepository restockingsRepository
        )
        {
            this.dbContext = dbContext;
            this.ordersRepository = ordersRepository;
            this.restockingsRepository = restockingsRepository;
        }

        public IQueryable<Order> FilterOrder(OpertaionsHistoriesListFilter filter)
        {
            IQueryable<Order> query = ordersRepository.Query();

            if (filter.InitialDate.HasValue)
            {
                query = query.Where(x => x.OrderDate.Date >= filter.InitialDate.Value.Date);
            }
            if (filter.FinalDate.HasValue)
            {
                query = query.Where(x => x.OrderDate.Date <= filter.FinalDate.Value.Date);
            }
            if(filter.ProductsIds != null && filter.ProductsIds.Length > 0)
            {
                query = query.Where(o => o.OrderProducts.Any(op => filter.ProductsIds.Contains(op.ProductId)));
            }

            return query;
        }

        public IQueryable<Restocking> FilterRestocking(OpertaionsHistoriesListFilter filter)
        {
            IQueryable<Restocking> query = restockingsRepository.Query();

            if (filter.InitialDate.HasValue)
            {
                query = query.Where(x => x.Date.Date >= filter.InitialDate.Value.Date);
            }
            if (filter.FinalDate.HasValue)
            {
                query = query.Where(x => x.Date.Date <= filter.FinalDate.Value.Date);
            }
            if(filter.ProductsIds != null && filter.ProductsIds.Length > 0)
            {
                query = query.Where(o => o.RestockingProducts.Any(op => filter.ProductsIds.Contains(op.ProductId)));
            }

            return query;
        }

        public async Task<OperationHistoryModel> ListAsync(IQueryable<Order> ordersQuery, IQueryable<Restocking> restockingsQuery)
        {

            var orderSummary = await ordersQuery.Select(x => new
            {
                ExpectedTotalProductsRevenue = x.OrderProducts.Sum(op => op.ExpectedTotalPrice),
                TotalProductsRevenue = x.OrderProducts.Sum(op => op.TotalPriceCharged),
                TotalChargedAdditionaFees = x.OrderAdditionalFees.Sum(af => af.Value),
                TotalDiscount = x.OrderProducts.Sum(op => op.TotalPriceCharged) * ((x.Discount ?? 0) / 100),
                TotalRevenue = x.OrderProducts.Sum(op => op.TotalPriceCharged) * (1 - ((x.Discount ?? 0) / 100)) + x.OrderAdditionalFees.Sum(af => af.Value)
            }).ToListAsync();


            List<double> restockingSummary = await restockingsQuery.Select(x => x.RestockingProducts.Sum(rp => rp.TotalPricePaid) + x.RestockingAdditionalFees.Sum(af => af.Value)).ToListAsync();

            double revenue = orderSummary.Any() ? orderSummary.Sum(x => x.TotalRevenue) : 0;
            double expense = restockingSummary.Any() ? restockingSummary.Sum() : 0;
            double profit = revenue - expense;
            double profitMargin = revenue == 0 ? 0 : Math.Round((profit / revenue) * 100, 2);

            return new OperationHistoryModel
            {
                ExpectedTotalProductsRevenue = orderSummary.Any() ? orderSummary.Sum(x => x.ExpectedTotalProductsRevenue) : 0,
                TotalProductsRevenue = orderSummary.Any() ? orderSummary.Sum(x => x.TotalProductsRevenue) : 0,
                TotalChargedAdditionalFees = orderSummary.Any() ? orderSummary.Sum(x => x.TotalChargedAdditionaFees) : 0,
                TotalDiscount = orderSummary.Any() ? (orderSummary.Sum(x => x.ExpectedTotalProductsRevenue) - orderSummary.Sum(x => x.TotalProductsRevenue)) * -1 : 0,
                TotalRevenue = revenue,
                TotalExpense = expense,
                TotalProfit = profit,
                TotalProfitMargin = profitMargin
            };
        }

    }
}
