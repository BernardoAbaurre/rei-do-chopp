using System.Linq;
using ReiDoChopp.Domain.RestockingProducts.Entities;
using ReiDoChopp.Domain.RestockingProducts.Repositories.Filters;
using ReiDoChopp.Domain.Utils;

namespace ReiDoChopp.Domain.RestockingProducts.Repositories
{
    public interface IRestockingProductsRepository : IEntityFrameworkRepository<RestockingProduct>
    {
        IQueryable<RestockingProduct>Filter(RestockingProductsListFilter filter);
    }
}
