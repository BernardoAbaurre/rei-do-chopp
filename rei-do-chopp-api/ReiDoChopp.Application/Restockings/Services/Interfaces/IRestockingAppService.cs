using System.Threading.Tasks;
using ReiDoChopp.DataTransfer.Restockings.Request;
using ReiDoChopp.DataTransfer.Restockings.Response;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Restockings.Responses;

namespace ReiDoChopp.Application.Restockings.Services.Interfaces
{
    public interface IRestockingsAppService
    {
        Task<RestockingResponse> EditAsync(int id, RestockingRequest request);

        Task<RestockingResponse> InsertAsync(RestockingRequest request);

        Task<PaginationResponse<RestockingHistoryResponse>> ListAsync(RestockingListRequest request);

        Task DeleteAsync(int restockingId);

        Task<RestockingResponse> ValidateAsync(int id);

        RestockingTotalsCalculationResponse CalculateTotals(RestockingRequest request);
    }
}
