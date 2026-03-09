using AutoMapper;
using ReiDoChopp.Application.OrderAdditionalFees.Services.Interfaces;
using ReiDoChopp.DataTransfer.OrderAdditionalFees.Request;
using ReiDoChopp.DataTransfer.OrderAdditionalFees.Response;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.Domain.OrderAdditionalFees.Entities;
using ReiDoChopp.Domain.OrderAdditionalFees.Repositories;
using ReiDoChopp.Domain.OrderAdditionalFees.Repositories.Filters;
using ReiDoChopp.Domain.OrderAdditionalFees.Services.Interfaces;
using ReiDoChopp.Domain.Utils.Models;
using ReiDoChopp.Infra.Data;

namespace ReiDoChopp.Application.OrderAdditionalFees.Services
{
    public class OrderAdditionalFeesAppService : IOrderAdditionalFeesAppService
    {
        private readonly IMapper mapper;
        private readonly ReiDoChoppDbContext dbContext;
        private readonly IOrderAdditionalFeesRepository orderAdditionalFeesRepository;
        private readonly IOrderAdditionalFeesService orderAdditionalFeesService;

        public OrderAdditionalFeesAppService(
            IMapper mapper,
            ReiDoChoppDbContext dbContext,
            IOrderAdditionalFeesRepository orderAdditionalFeesRepository,
            IOrderAdditionalFeesService orderAdditionalFeesService)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.orderAdditionalFeesRepository = orderAdditionalFeesRepository;
            this.orderAdditionalFeesService = orderAdditionalFeesService;
        }



        public async Task<PaginationResponse<OrderAdditionalFeeResponse>> ListAsync(OrderAdditionalFeeListRequest request)
        {
            OrderAdditionalFeesListFilter filter = mapper.Map<OrderAdditionalFeesListFilter>(request);

            IQueryable<OrderAdditionalFee> query = orderAdditionalFeesRepository.Filter(filter);

            PaginationModel<OrderAdditionalFee> response = await orderAdditionalFeesRepository.ListAsync(query, filter);

            return mapper.Map<PaginationResponse<OrderAdditionalFeeResponse>>(response);
        }

        public async Task<OrderAdditionalFeeResponse> ValidateAsync(int id)
        {
            OrderAdditionalFee entity = await orderAdditionalFeesService.ValidateAsync(id);
            return mapper.Map<OrderAdditionalFeeResponse>(entity);
        }
    }
}
