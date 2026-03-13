using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ReiDoChopp.Application.Products.Services.Interfaces;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Products.Request;
using ReiDoChopp.DataTransfer.Products.Response;
using ReiDoChopp.Domain.Products.Entities;
using ReiDoChopp.Domain.Products.Repositories;
using ReiDoChopp.Domain.Products.Repositories.Filters;
using ReiDoChopp.Domain.Products.Repositories.Models;
using ReiDoChopp.Domain.Products.Services.Interfaces;
using ReiDoChopp.Domain.Utils.Models;
using ReiDoChopp.Infra.Data;

namespace ReiDoChopp.Application.Products.Services
{
    public class ProductsAppService : IProductsAppService
    {
        private readonly IMapper mapper;
        private readonly IProductsRepository productsRepository;
        private readonly IProductsService productsService;
        private readonly ReiDoChoppDbContext dbContext;

        public ProductsAppService(
            IMapper mapper,
            IProductsRepository productsRepository,
            IProductsService productsService,
            ReiDoChoppDbContext dbContext)
        {
            this.mapper = mapper;
            this.productsRepository = productsRepository;
            this.productsService = productsService;
            this.dbContext = dbContext;
        }

        public async Task<ProductResponse> EditAsync(int id, ProductEditRequest request)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {

                Product entity = await productsService.EditAsync(id, request.BarCode, request.Description, request.SellingPrice, request.AlertQuantity);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return mapper.Map<ProductResponse>(entity);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ProductResponse> InsertAsync(ProductInsertRequest request)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                Product entity = await productsService.InsertAsync(request.BarCode, request.Description, request.SellingPrice, request.StockQuantity, request.AlertQuantity);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return mapper.Map<ProductResponse>(entity);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PaginationResponse<ProductResponse>> ListAsync(ProductListRequest request)
        {
            ProductsListFilter filter = mapper.Map<ProductsListFilter>(request);

            IQueryable<Product> query = productsRepository.Filter(filter);

            PaginationModel<ProductModel> model = await productsRepository.ListProjection(query, filter);

            return mapper.Map<PaginationResponse<ProductResponse>>(model);
        }

        public async Task<ProductResponse> ValidateAsync(int id)
        {
            Product entity = await productsService.ValidateAsync(id);
            return mapper.Map<ProductResponse>(entity);
        }

        public async Task<ProductResponse> ChangeStatusAsync(int id)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                Product product = await productsService.ValidateAsync(id);

                productsService.ChangeStatus(product);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return mapper.Map<ProductResponse>(product);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
