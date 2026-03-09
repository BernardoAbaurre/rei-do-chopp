using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using AutoMapper;
using ReiDoChopp.Application.OrdersProducts.Services.Interfaces;
using ReiDoChopp.DataTransfer.OrdersProducts.Request;
using ReiDoChopp.DataTransfer.OrdersProducts.Response;
using ReiDoChopp.Domain.OrdersProducts.Entities;
using ReiDoChopp.Domain.OrdersProducts.Repositories;
using ReiDoChopp.Domain.OrdersProducts.Repositories.Filters;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.Domain.OrdersProducts.Services.Interfaces;
using ReiDoChopp.Infra.Data;
using ReiDoChopp.Domain.Utils.Models;

namespace ReiDoChopp.Application.OrdersProducts.Services
{
    public class OrdersProductsAppService : IOrdersProductsAppService
    {
        private readonly IMapper mapper;
        private readonly ReiDoChoppDbContext dbContext;
        private readonly IOrdersProductsRepository ordersProductsRepository;
        private readonly IOrdersProductsService ordersProductsService;

        public OrdersProductsAppService(
            IMapper mapper,
            ReiDoChoppDbContext dbContext,
            IOrdersProductsRepository ordersProductsRepository,
            IOrdersProductsService ordersProductsService)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.ordersProductsRepository = ordersProductsRepository;
            this.ordersProductsService = ordersProductsService;
        }

        public async Task<PaginationResponse<OrderProductHistoryResponse>> ListAsync(OrderProductListRequest request)
        {
            OrdersProductsListFilter filter = mapper.Map<OrdersProductsListFilter>(request);

            IQueryable<OrderProduct> query = ordersProductsRepository.Filter(filter);

            PaginationModel<OrderProduct> response = await ordersProductsRepository.ListAsync(query, filter);

            return mapper.Map<PaginationResponse<OrderProductHistoryResponse>>(response);
        }

        public async Task<OrderProductResponse> ValidateAsync(int id)
        {
            OrderProduct entity = await ordersProductsService.ValidateAsync(id);
            return mapper.Map<OrderProductResponse>(entity);
        }
    }
}
