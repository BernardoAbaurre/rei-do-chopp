using System.Threading.Tasks;
using ReiDoChopp.DataTransfer.RestockingProducts.Requests;
using ReiDoChopp.DataTransfer.RestockingProducts.Response;
using ReiDoChopp.DataTransfer.Pagination.Responses;

namespace ReiDoChopp.Application.RestockingProducts.Services.Interfaces
{
    public interface IRestockingProductsAppService
    {
        Task<PaginationResponse<RestockingProductHistoryResponse>> ListAsync(RestockingProductListRequest request);

        Task<RestockingProductResponse> ValidateAsync(int id);
    }
}
