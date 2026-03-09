using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using ReiDoChopp.Application.Orders.Services.Interfaces;
using ReiDoChopp.DataTransfer.Orders.Request;
using ReiDoChopp.DataTransfer.Orders.Response;
using ReiDoChopp.DataTransfer.Orders.Responses;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.PrintControls.Response;
using ReiDoChopp.Domain.Orders.Entities;
using ReiDoChopp.Domain.Orders.Repositories;
using ReiDoChopp.Domain.Orders.Repositories.Filters;
using ReiDoChopp.Domain.Orders.Repositories.Models;
using ReiDoChopp.Domain.Orders.Services.Commands;
using ReiDoChopp.Domain.Orders.Services.Interfaces;
using ReiDoChopp.Domain.OrdersProducts.Services.Interfaces;
using ReiDoChopp.Domain.PrintControls.Entities;
using ReiDoChopp.Domain.Products.Repositories;
using ReiDoChopp.Domain.Users.Entities;
using ReiDoChopp.Domain.Users.Services.Interfaces;
using ReiDoChopp.Domain.Utils.Models;
using ReiDoChopp.Infra.Data;

namespace ReiDoChopp.Application.Orders.Services
{
    public class OrdersAppService : IOrdersAppService
    {
        private readonly IMapper mapper;
        private readonly ReiDoChoppDbContext dbContext;
        private readonly IOrdersRepository ordersRepository;
        private readonly IOrdersService ordersService;
        private readonly IUsersService usersService;
        private readonly IProductsRepository productsRepository;
        private readonly IOrdersProductsService ordersProductsService;

        public OrdersAppService(
            IMapper mapper,
            ReiDoChoppDbContext dbContext,
            IOrdersRepository ordersRepository,
            IOrdersService ordersService,
            IUsersService usersService,
            IProductsRepository productsRepository,
            IOrdersProductsService ordersProductsService
        )
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.ordersRepository = ordersRepository;
            this.ordersService = ordersService;
            this.usersService = usersService;
            this.productsRepository = productsRepository;
            this.ordersProductsService = ordersProductsService;
        }

        public async Task<OrderResponse> EditAsync(int id, OrderRequest request)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                OrderCommand command = mapper.Map<OrderCommand>(request);

                command.Cashier = await usersService.GetCurrentUserAsync();

                command.Attendant = request.AttendantId.HasValue ? await usersService.ValidateAsync(request.AttendantId.Value) : null;

                Order entity = await ordersService.EditAsync(id, command);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return mapper.Map<OrderResponse>(entity);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<OrderResponse> InsertAsync(OrderRequest request)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                OrderCommand command = mapper.Map<OrderCommand>(request);

                command.Cashier = await usersService.GetCurrentUserAsync();

                command.Attendant = request.AttendantId.HasValue ? await usersService.ValidateAsync(request.AttendantId.Value) : null;

                Order order = await ordersService.InsertAsync(command);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return mapper.Map<OrderResponse>(order);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PaginationResponse<OrderHistoryResponse>> ListAsync(OrderListRequest request)
        {
            OrdersListFilter filter = mapper.Map<OrdersListFilter>(request);

            IQueryable<Order> query = ordersRepository.Filter(filter);

            PaginationModel<OrderHistoryModel> model = await ordersRepository.GetOrderHistory(query, filter);

            return mapper.Map<PaginationResponse<OrderHistoryResponse>>(model);
        }

        public async Task<OrderResponse> ValidateAsync(int id)
        {
            Order entity = await ordersService.ValidateAsync(id);
            return mapper.Map<OrderResponse>(entity);
        }

        public async Task DeleteAsync(int orderId)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                Order order = await ordersService.ValidateAsync(orderId);

                ordersProductsService.Delete(order);

                ordersRepository.Delete(order);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public OrderTotalsCalculationResponse CalculateTotals(OrderRequest request)
        {
            OrderCommand command = mapper.Map<OrderCommand>(request);

            OrderTotalsCalculationModel result = ordersRepository.CalculateTotals(command);

            return mapper.Map<OrderTotalsCalculationResponse>(result);
        }

        public async Task<PrintControlResponse> PrintAsync(int orderId)
        {

            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                Order order = await ordersService.ValidateAsync(orderId);

                User user = await usersService.GetCurrentUserAsync();

                PrintControl result = await ordersService.PrintAsync(order, user);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return mapper.Map<PrintControlResponse>(result);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            

        }
    }
}
