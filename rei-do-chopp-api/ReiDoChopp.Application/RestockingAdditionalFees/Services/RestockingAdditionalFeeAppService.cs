using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using AutoMapper;
using ReiDoChopp.Application.RestockingAdditionalFees.Services.Interfaces;
using ReiDoChopp.DataTransfer.RestockingAdditionalFees.Requests;
using ReiDoChopp.DataTransfer.RestockingAdditionalFees.Response;
using ReiDoChopp.Domain.RestockingAdditionalFees.Entities;
using ReiDoChopp.Domain.RestockingAdditionalFees.Repositories;
using ReiDoChopp.Domain.RestockingAdditionalFees.Repositories.Filters;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.Domain.RestockingAdditionalFees.Services.Interfaces;
using ReiDoChopp.Infra.Data;
using ReiDoChopp.Domain.Utils.Models;

namespace ReiDoChopp.Application.RestockingAdditionalFees.Services
{
    public class RestockingAdditionalFeesAppService : IRestockingAdditionalFeesAppService
    {
        private readonly IMapper mapper;
        private readonly ReiDoChoppDbContext dbContext;
        private readonly IRestockingAdditionalFeesRepository restockingAdditionalFeesRepository;
        private readonly IRestockingAdditionalFeesService restockingAdditionalFeesService;

        public RestockingAdditionalFeesAppService(
            IMapper mapper,
            ReiDoChoppDbContext dbContext,
            IRestockingAdditionalFeesRepository restockingAdditionalFeesRepository,
            IRestockingAdditionalFeesService restockingAdditionalFeesService)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.restockingAdditionalFeesRepository = restockingAdditionalFeesRepository;
            this.restockingAdditionalFeesService = restockingAdditionalFeesService;
        }
        public async Task<PaginationResponse<RestockingAdditionalFeeResponse>> ListAsync(RestockingAdditionalFeeListRequest request)
        {
            RestockingAdditionalFeesListFilter filter = mapper.Map<RestockingAdditionalFeesListFilter>(request);

            IQueryable<RestockingAdditionalFee> query = restockingAdditionalFeesRepository.Filter(filter);

            PaginationModel<RestockingAdditionalFee> response = await restockingAdditionalFeesRepository.ListAsync(query, filter);

            return mapper.Map<PaginationResponse<RestockingAdditionalFeeResponse>>(response);
        }

        public async Task<RestockingAdditionalFeeResponse> ValidateAsync(int id)
        {
            RestockingAdditionalFee entity = await restockingAdditionalFeesService.ValidateAsync(id);
            return mapper.Map<RestockingAdditionalFeeResponse>(entity);
        }
    }
}
