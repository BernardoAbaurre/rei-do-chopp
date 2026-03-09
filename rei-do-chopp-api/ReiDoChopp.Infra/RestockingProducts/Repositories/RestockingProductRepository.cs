using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReiDoChopp.Domain.RestockingProducts.Entities;
using ReiDoChopp.Domain.RestockingProducts.Repositories;
using ReiDoChopp.Domain.RestockingProducts.Repositories.Filters;
using ReiDoChopp.Infra.Utils;
using ReiDoChopp.Infra.Data;

namespace ReiDoChopp.Infra.RestockingProducts.Repositories
{
    public class RestockingProductsRepository : EntityFrameworkRepository<RestockingProduct>, IRestockingProductsRepository
    {
        public RestockingProductsRepository(ReiDoChoppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<RestockingProduct> Filter(RestockingProductsListFilter filter)
        {
            var query = Query();
            
            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (filter.RestockingId.HasValue)
            {
                query = query.Where(x => x.Restocking.Id == filter.RestockingId.Value);
            }
            if (filter.ProductId.HasValue)
            {
                query = query.Where(x => x.Product.Id == filter.ProductId.Value);
            }
            if (filter.Quantity.HasValue)
            {
                query = query.Where(x => x.Quantity == filter.Quantity.Value);
            }
            if (filter.UnitPricePaid.HasValue)
            {
                query = query.Where(x => x.UnitPricePaid == filter.UnitPricePaid.Value);
            }

            return query;
        }
    }
}
