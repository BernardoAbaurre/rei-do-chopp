using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReiDoChopp.Domain.Products.Entities;
using ReiDoChopp.Domain.Products.Repositories;
using ReiDoChopp.Domain.Products.Repositories.Filters;
using ReiDoChopp.Domain.Products.Repositories.Models;
using ReiDoChopp.Domain.Utils.Extensions;
using ReiDoChopp.Domain.Utils.Models;
using ReiDoChopp.Infra.Data;
using ReiDoChopp.Infra.Utils;

namespace ReiDoChopp.Infra.Products.Repositories
{
    public class ProductsRepository : EntityFrameworkRepository<Product>, IProductsRepository
    {
        public ProductsRepository(ReiDoChoppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Product> Filter(ProductsListFilter filter)
        {
            var query = Query();

            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id.Equals(filter.Id));
            }
            if (!string.IsNullOrEmpty(filter.BarCode))
            {
                query = query.Where(x => x.BarCode.Equals(filter.BarCode));
            }
            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(x => x.Description.Contains(filter.Description));
            }
            if (filter.SellingPrice.HasValue)
            {
                query = query.Where(x => x.SellingPrice == filter.SellingPrice.Value);
            }
            if (filter.StockQuantity.HasValue)
            {
                query = query.Where(x => x.StockQuantity == filter.StockQuantity.Value);
            }
            if (filter.Active.HasValue)
            {
                query = query.Where(x => x.Active == filter.Active.Value);
            }
            if (!string.IsNullOrEmpty(filter.DescriptionOrBarCode))
            {
                query = query.Where(x => x.BarCode.Equals(filter.DescriptionOrBarCode) || x.Description.Contains(filter.DescriptionOrBarCode));
            }
            if (filter.Alert.HasValue && filter.Alert.Value)
            {
                query = query.Where(x => x.StockQuantity <= x.AlertQuantity);
            } 
            if (filter.Alert.HasValue && !filter.Alert.Value)
            {
                query = query.Where(x => x.StockQuantity > x.AlertQuantity);
            }

            return query;
        }

        public async Task<PaginationModel<ProductModel>> ListProjection(IQueryable<Product> query, ProductsListFilter filter)
        {
            return await query.Select(x => new ProductModel
            {
                Id = x.Id,
                Description = x.Description,
                BarCode = x.BarCode,
                SellingPrice = x.SellingPrice,
                StockQuantity = x.StockQuantity,
                AlertQuantity = x.AlertQuantity,
                Active = x.Active,
                Alert = x.StockQuantity <= x.AlertQuantity
            }).PageAsync(filter);
        }
    }
}
