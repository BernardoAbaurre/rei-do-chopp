using System.Threading.Tasks;
using ReiDoChopp.DataTransfer.OrdersProducts.Request;
using ReiDoChopp.DataTransfer.OrdersProducts.Response;
using ReiDoChopp.DataTransfer.Pagination.Responses;

namespace ReiDoChopp.Application.OrdersProducts.Services.Interfaces
{
    public interface IOrdersProductsAppService
    {
        Task<PaginationResponse<OrderProductHistoryResponse>> ListAsync(OrderProductListRequest request);

        Task<OrderProductResponse> ValidateAsync(int id);
    }
}
