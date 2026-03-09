using ReiDoChopp.DataTransfer.Orders.Request;
using ReiDoChopp.DataTransfer.Orders.Response;
using ReiDoChopp.DataTransfer.Orders.Responses;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.PrintControls.Response;
using ReiDoChopp.Domain.PrintControls.Entities;

namespace ReiDoChopp.Application.Orders.Services.Interfaces
{
    public interface IOrdersAppService
    {
        Task<OrderResponse> EditAsync(int id, OrderRequest request);

        Task<OrderResponse> InsertAsync(OrderRequest request);

        Task<PaginationResponse<OrderHistoryResponse>> ListAsync(OrderListRequest request);

        Task<OrderResponse> ValidateAsync(int id);

        Task DeleteAsync(int orderId);
        OrderTotalsCalculationResponse CalculateTotals(OrderRequest request);
        Task<PrintControlResponse> PrintAsync(int orderId);
    }
}
