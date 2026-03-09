using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.RestockingAdditionalFees.Requests;
using ReiDoChopp.DataTransfer.RestockingAdditionalFees.Response;

namespace ReiDoChopp.Application.RestockingAdditionalFees.Services.Interfaces
{
    public interface IRestockingAdditionalFeesAppService
    {
        Task<PaginationResponse<RestockingAdditionalFeeResponse>> ListAsync(RestockingAdditionalFeeListRequest request);

        Task<RestockingAdditionalFeeResponse> ValidateAsync(int id);
    }
}
