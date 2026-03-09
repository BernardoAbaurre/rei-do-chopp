using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using ReiDoChopp.Application.Restockings.Services.Interfaces;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Restockings.Request;
using ReiDoChopp.DataTransfer.Restockings.Response;
using ReiDoChopp.DataTransfer.Restockings.Responses;
using ReiDoChopp.Domain.Orders.Repositories.Models;
using ReiDoChopp.Domain.RestockingProducts.Services.Interfaces;
using ReiDoChopp.Domain.Restockings.Entities;
using ReiDoChopp.Domain.Restockings.Repositories;
using ReiDoChopp.Domain.Restockings.Repositories.Filters;
using ReiDoChopp.Domain.Restockings.Repositories.Models;
using ReiDoChopp.Domain.Restockings.Services.Commands;
using ReiDoChopp.Domain.Restockings.Services.Interfaces;
using ReiDoChopp.Domain.Users.Services.Interfaces;
using ReiDoChopp.Domain.Utils.Models;
using ReiDoChopp.Infra.Data;

namespace ReiDoChopp.Application.Restockings.Services
{
    public class RestockingsAppService : IRestockingsAppService
    {
        private readonly IMapper mapper;
        private readonly ReiDoChoppDbContext dbContext;
        private readonly IRestockingsRepository restockingsRepository;
        private readonly IRestockingsService restockingsService;
        private readonly IUsersService usersService;
        private readonly IRestockingProductsService restockingProductsService;

        public RestockingsAppService(
            IMapper mapper,
            ReiDoChoppDbContext dbContext,
            IRestockingsRepository restockingsRepository,
            IRestockingsService restockingsService,
            IUsersService usersService,
            IRestockingProductsService restockingProductsService
            )
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.restockingsRepository = restockingsRepository;
            this.restockingsService = restockingsService;
            this.usersService = usersService;
            this.restockingProductsService = restockingProductsService;
        }

        public async Task<RestockingResponse> EditAsync(int id, RestockingRequest request)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                RestockingCommand command = mapper.Map<RestockingCommand>(request);

                command.User = await usersService.GetCurrentUserAsync();

                Restocking entity = await restockingsService.EditAsync(id, command);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return mapper.Map<RestockingResponse>(entity);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<RestockingResponse> InsertAsync(RestockingRequest request)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                RestockingCommand command = mapper.Map<RestockingCommand>(request);

                command.User = await usersService.GetCurrentUserAsync();
                Restocking entity = await restockingsService.InsertAsync(command);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return mapper.Map<RestockingResponse>(entity);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PaginationResponse<RestockingHistoryResponse>> ListAsync(RestockingListRequest request)
        {
            RestockingsListFilter filter = mapper.Map<RestockingsListFilter>(request);

            IQueryable<Restocking> query = restockingsRepository.Filter(filter);

            PaginationModel<RestockingHistoryModel> response = await restockingsRepository.GetRestockingHistory(query, filter);

            return mapper.Map<PaginationResponse<RestockingHistoryResponse>>(response);
        }

        public async Task DeleteAsync(int restockingId)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                Restocking restocking = await restockingsService.ValidateAsync(restockingId);

                restockingProductsService.Delete(restocking);

                restockingsRepository.Delete(restocking);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<RestockingResponse> ValidateAsync(int id)
        {
            Restocking entity = await restockingsService.ValidateAsync(id);
            return mapper.Map<RestockingResponse>(entity);
        }

        public RestockingTotalsCalculationResponse CalculateTotals(RestockingRequest request)
        {
            RestockingCommand command = mapper.Map<RestockingCommand>(request);

            RestockingTotalsCalculationModel result = restockingsRepository.CalculateTotals(command);

            return mapper.Map<RestockingTotalsCalculationResponse>(result);
        }
    }
}
