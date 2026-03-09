using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using AutoMapper;
using ReiDoChopp.Application.PrintControls.Services.Interfaces;
using ReiDoChopp.DataTransfer.PrintControls.Requests;
using ReiDoChopp.DataTransfer.PrintControls.Response;
using ReiDoChopp.Domain.PrintControls.Entities;
using ReiDoChopp.Domain.PrintControls.Repositories;
using ReiDoChopp.Domain.PrintControls.Repositories.Filters;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.Domain.PrintControls.Services.Interfaces;
using ReiDoChopp.Infra.Data;
using ReiDoChopp.Domain.Utils.Models;
using ReiDoChopp.DataTransfer.PrintControls.Requests;
using ReiDoChopp.Domain.Users.Services.Interfaces;
using ReiDoChopp.Domain.Users.Entities;
using ReiDoChopp.Domain.PrintControls.Enums;
using Microsoft.EntityFrameworkCore;

namespace ReiDoChopp.Application.PrintControls.Services
{
    public class PrintControlsAppService : IPrintControlsAppService
    {
        private readonly IMapper mapper;
        private readonly ReiDoChoppDbContext dbContext;
        private readonly IPrintControlsRepository printControlsRepository;
        private readonly IPrintControlsService printControlsService;
        private readonly IUsersService usersService;

        public PrintControlsAppService(
            IMapper mapper,
            ReiDoChoppDbContext dbContext,
            IPrintControlsRepository printControlsRepository,
            IPrintControlsService printControlsService,
            IUsersService usersService)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.printControlsRepository = printControlsRepository;
            this.printControlsService = printControlsService;
            this.usersService = usersService;
        }

        public async Task<PrintControlResponse> ChangeStatusAsync(int id, PrintControlChangeStatusRequest request)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();
            
            try
            {
                PrintControl entity = await printControlsService.ChangeStatusAsync(id, request.Status);
                
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                    
                return mapper.Map<PrintControlResponse>(entity);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PrintControlResponse> InsertAsync(PrintControlInsertRequest request)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                User user = await usersService.GetCurrentUserAsync();

                PrintControl entity = await printControlsService.InsertAsync(user, request.Content);
                
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return mapper.Map<PrintControlResponse>(entity);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PaginationResponse<PrintControlResponse>> ListAsync(PrintControlListRequest request)
        {
            PrintControlsListFilter filter = mapper.Map<PrintControlsListFilter>(request);

            IQueryable<PrintControl> query = printControlsRepository.Filter(filter);

            PaginationModel<PrintControl> response = await printControlsRepository.ListAsync(query, filter);

            return mapper.Map<PaginationResponse<PrintControlResponse>>(response);
        }

        public async Task<PrintControlResponse> GetLastAsync()
        {
            IQueryable<PrintControl> query = printControlsRepository.Query().Where(x => x.Status == PrintControlStatusEnum.Requested).OrderByDescending(x => x.RequestDate);

            PrintControl response = await query.FirstOrDefaultAsync();

            return mapper.Map<PrintControlResponse>(response);
        }

        public async Task<PrintControlResponse> ValidateAsync(int id)
        {
            PrintControl entity = await printControlsService.ValidateAsync(id);
            return mapper.Map<PrintControlResponse>(entity);
        }
    }
}
