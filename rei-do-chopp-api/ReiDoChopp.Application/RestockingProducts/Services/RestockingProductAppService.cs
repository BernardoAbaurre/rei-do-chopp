using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using AutoMapper;
using ReiDoChopp.Application.RestockingProducts.Services.Interfaces;
using ReiDoChopp.DataTransfer.RestockingProducts.Requests;
using ReiDoChopp.DataTransfer.RestockingProducts.Response;
using ReiDoChopp.Domain.RestockingProducts.Entities;
using ReiDoChopp.Domain.RestockingProducts.Repositories;
using ReiDoChopp.Domain.RestockingProducts.Repositories.Filters;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.Domain.RestockingProducts.Services.Interfaces;
using ReiDoChopp.Infra.Data;
using ReiDoChopp.Domain.Utils.Models;

namespace ReiDoChopp.Application.RestockingProducts.Services
{
    public class RestockingProductsAppService : IRestockingProductsAppService
    {
        private readonly IMapper mapper;
        private readonly ReiDoChoppDbContext dbContext;
        private readonly IRestockingProductsRepository restockingProductsRepository;
        private readonly IRestockingProductsService restockingProductsService;

        public RestockingProductsAppService(
            IMapper mapper,
            ReiDoChoppDbContext dbContext,
            IRestockingProductsRepository restockingProductsRepository,
            IRestockingProductsService restockingProductsService)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.restockingProductsRepository = restockingProductsRepository;
            this.restockingProductsService = restockingProductsService;
        }

        public async Task<PaginationResponse<RestockingProductHistoryResponse>> ListAsync(RestockingProductListRequest request)
        {
            RestockingProductsListFilter filter = mapper.Map<RestockingProductsListFilter>(request);

            IQueryable<RestockingProduct> query = restockingProductsRepository.Filter(filter);

            PaginationModel<RestockingProduct> response = await restockingProductsRepository.ListAsync(query, filter);

            return mapper.Map<PaginationResponse<RestockingProductHistoryResponse>>(response);
        }

        public async Task<RestockingProductResponse> ValidateAsync(int id)
        {
            RestockingProduct entity = await restockingProductsService.ValidateAsync(id);
            return mapper.Map<RestockingProductResponse>(entity);
        }
    }
}
