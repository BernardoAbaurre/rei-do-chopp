using System.Linq;
using ReiDoChopp.Domain.Products.Entities;
using ReiDoChopp.Domain.Products.Repositories.Filters;
using ReiDoChopp.Domain.Products.Repositories.Models;
using ReiDoChopp.Domain.Utils;
using ReiDoChopp.Domain.Utils.Models;

namespace ReiDoChopp.Domain.Products.Repositories
{
    public interface IProductsRepository : IEntityFrameworkRepository<Product>
    {
        IQueryable<Product>Filter(ProductsListFilter filter);
        Task<PaginationModel<ProductModel>> ListProjection(IQueryable<Product> query, ProductsListFilter filter);
    }
}
