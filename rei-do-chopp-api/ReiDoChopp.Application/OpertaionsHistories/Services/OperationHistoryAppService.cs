using AutoMapper;
using ReiDoChopp.Application.OpertaionsHistories.Services.Interfaces;
using ReiDoChopp.DataTransfer.OpertaionsHistories.Requests;
using ReiDoChopp.DataTransfer.OpertaionsHistories.Response;
using ReiDoChopp.Domain.OpertaionsHistories.Repositories;
using ReiDoChopp.Domain.OpertaionsHistories.Repositories.Filters;
using ReiDoChopp.Domain.OpertaionsHistories.Repositories.Models;
using ReiDoChopp.Domain.Orders.Entities;
using ReiDoChopp.Domain.Restockings.Entities;
using ReiDoChopp.Infra.Data;

namespace ReiDoChopp.Application.OpertaionsHistories.Services
{
    public class OpertaionsHistoriesAppService : IOpertaionsHistoriesAppService
    {
        private readonly IMapper mapper;
        private readonly ReiDoChoppDbContext dbContext;
        private readonly IOpertaionsHistoriesRepository opertaionsHistoriesRepository;

        public OpertaionsHistoriesAppService(
            IMapper mapper,
            ReiDoChoppDbContext dbContext,
            IOpertaionsHistoriesRepository opertaionsHistoriesRepository
        )
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.opertaionsHistoriesRepository = opertaionsHistoriesRepository;
        }


        public async Task<OperationHistoryResponse> ListAsync(OperationHistoryListRequest request)
        {
            OpertaionsHistoriesListFilter filter = mapper.Map<OpertaionsHistoriesListFilter>(request);

            IQueryable<Order> orderQuery = opertaionsHistoriesRepository.FilterOrder(filter);
            IQueryable<Restocking> restockingQuery = opertaionsHistoriesRepository.FilterRestocking(filter);

            OperationHistoryModel response = await opertaionsHistoriesRepository.ListAsync(orderQuery, restockingQuery);

            return mapper.Map<OperationHistoryResponse>(response);
        }
    }
}
