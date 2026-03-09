using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ReiDoChopp.DataTransfer.Settings;
using ReiDoChopp.Domain.Orders.Repositories;
using ReiDoChopp.Domain.Products.Repositories;
using ReiDoChopp.Domain.Restockings.Repositories;
using ReiDoChopp.Domain.Users.Services.Interfaces;
using ReiDoChopp.Infra.Data;

namespace ReiDoChopp.Application.Settings.Services
{
    public class SettingsAppService : ISettingsAppService
    {
        private readonly IMapper mapper;
        private readonly ReiDoChoppDbContext dbContext;
        private readonly IProductsRepository productsRepository;
        private readonly IUsersService usersService;
        private readonly IOrdersRepository ordersRepository;
        private readonly IRestockingsRepository restockingsRepository;

        public SettingsAppService(
            IMapper mapper,
            ReiDoChoppDbContext dbContext,
            IProductsRepository productsRepository,
            IUsersService usersService,
            IOrdersRepository ordersRepository,
            IRestockingsRepository restockingsRepository
        )
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.productsRepository = productsRepository;
            this.usersService = usersService;
            this.ordersRepository = ordersRepository;
            this.restockingsRepository = restockingsRepository;
        }


        public async Task ResetStockingHistory(SettingsAuthRequest request)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                if (!await usersService.ConfirmCurrentUserPassword(request.Password))
                    throw new Exception("Senha incorreta");

                await productsRepository.Query().ExecuteUpdateAsync(x => x.SetProperty(p => p.StockQuantity, 0));

                await ordersRepository.Query().ExecuteDeleteAsync();
                await restockingsRepository.Query().ExecuteDeleteAsync();

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task ResetBase(SettingsAuthRequest request)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                if (!await usersService.ConfirmCurrentUserPassword(request.Password))
                    throw new Exception("Senha incorreta");

                await ordersRepository.Query().ExecuteDeleteAsync();
                await restockingsRepository.Query().ExecuteDeleteAsync();
                await productsRepository.Query().ExecuteDeleteAsync();

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
