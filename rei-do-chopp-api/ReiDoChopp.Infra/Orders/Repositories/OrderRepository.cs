using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReiDoChopp.Domain.Orders.Entities;
using ReiDoChopp.Domain.Orders.Repositories;
using ReiDoChopp.Domain.Orders.Repositories.Filters;
using ReiDoChopp.Domain.Orders.Repositories.Models;
using ReiDoChopp.Domain.Orders.Services.Commands;
using ReiDoChopp.Domain.OrdersProducts.Entities;
using ReiDoChopp.Domain.OrdersProducts.Repositories.Filters;
using ReiDoChopp.Domain.Utils.Extensions;
using ReiDoChopp.Domain.Utils.Models;
using ReiDoChopp.Infra.Data;
using ReiDoChopp.Infra.Utils;

namespace ReiDoChopp.Infra.Orders.Repositories
{
    public class OrdersRepository : EntityFrameworkRepository<Order>, IOrdersRepository
    {
        public OrdersRepository(ReiDoChoppDbContext dbContext) : base(dbContext)
        {
        }


        public IQueryable<Order> Filter(OrdersListFilter filter)
        {
            var query = Query();

            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (filter.CashierId.HasValue)
            {
                query = query.Where(x => x.Cashier.Id == filter.CashierId.Value);
            }
            if (filter.AttendantId.HasValue)
            {
                query = query.Where(x => x.Attendant.Id == filter.AttendantId.Value);
            }
            if (filter.InitialDate.HasValue)
            {
                query = query.Where(x => x.OrderDate.Date >= filter.InitialDate.Value.Date);
            }
            if (filter.FinalDate.HasValue)
            {
                query = query.Where(x => x.OrderDate.Date <= filter.FinalDate.Value.Date);
            }
            if (filter.Discount.HasValue)
            {
                query = query.Where(x => x.Discount == filter.Discount.Value);
            }
            if(filter.ProductsIds != null && filter.ProductsIds.Length > 0)
            {
                query = query.Where(o => o.OrderProducts.Any(op => filter.ProductsIds.Contains(op.ProductId)));
            }

            return query;
        }
        public async Task<PaginationModel<OrderHistoryModel>> GetOrderHistory(IQueryable<Order> query, OrdersListFilter filter)
        {
            return await query.Select(o => new OrderHistoryModel
            {
                Id = o.Id,
                Cashier = o.Cashier.FirstName + " " + o.Cashier.LastName,
                Attendant = o.Attendant.FirstName + " " + o.Attendant.LastName,
                OrderDate = o.OrderDate,
                ExpectedProductsSumValue = o.OrderProducts.Sum(op => op.ExpectedTotalPrice),
                RealProductsSumValue = o.OrderProducts.Sum(op => op.TotalPriceCharged),
                Discount = o.OrderProducts.Sum(op => op.ExpectedTotalPrice) - o.OrderProducts.Sum(op => op.TotalPriceCharged),
                TotalFees = o.OrderAdditionalFees.Sum(af => af.Value),
                TotalValue = o.OrderProducts.Sum(op => op.TotalPriceCharged) * (1 - ((o.Discount ?? 0) / 100)) + o.OrderAdditionalFees.Sum(af => af.Value)
            }).PageAsync(filter); 
        }
        public OrderTotalsCalculationModel CalculateTotals(OrderCommand command)
        {
            double totalProducts = command.OrderProducts?.Sum(op => op.UnitPriceCharged * op.Quantity) ?? 0;
            double expectedTotalProducts = command.OrderProducts?.Sum(op => op.UnitPrice * op.Quantity) ?? 0;
            double totalFees = command.OrderAdditionalFees?.Sum(af => af.Value) ?? 0;

            return new OrderTotalsCalculationModel
            {
                ExpectedTotalProducts = expectedTotalProducts,
                TotalProducts = totalProducts,
                TotalFees = totalFees,
                ExpectedTotal = expectedTotalProducts + totalFees,
                Total = totalProducts + totalFees
            };
        }
    }
}
