using System.Threading.Tasks;
using ReiDoChopp.DataTransfer.OrderAdditionalFees.Request;
using ReiDoChopp.DataTransfer.OrderAdditionalFees.Response;
using ReiDoChopp.DataTransfer.Pagination.Responses;

namespace ReiDoChopp.Application.OrderAdditionalFees.Services.Interfaces
{
    public interface IOrderAdditionalFeesAppService
    {
        Task<PaginationResponse<OrderAdditionalFeeResponse>> ListAsync(OrderAdditionalFeeListRequest request);

        Task<OrderAdditionalFeeResponse> ValidateAsync(int id);
    }
}
