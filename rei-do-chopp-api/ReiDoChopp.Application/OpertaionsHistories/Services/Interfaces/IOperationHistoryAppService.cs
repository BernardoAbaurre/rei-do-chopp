using ReiDoChopp.DataTransfer.OpertaionsHistories.Requests;
using ReiDoChopp.DataTransfer.OpertaionsHistories.Response;

namespace ReiDoChopp.Application.OpertaionsHistories.Services.Interfaces
{
    public interface IOpertaionsHistoriesAppService
    {
        Task<OperationHistoryResponse> ListAsync(OperationHistoryListRequest request);
    }
}
