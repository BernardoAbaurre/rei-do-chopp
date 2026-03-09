using System.Threading.Tasks;
using ReiDoChopp.DataTransfer.PrintControls.Requests;
using ReiDoChopp.DataTransfer.PrintControls.Response;
using ReiDoChopp.DataTransfer.Pagination.Responses;

namespace ReiDoChopp.Application.PrintControls.Services.Interfaces
{
    public interface IPrintControlsAppService
    {
        Task<PrintControlResponse> ChangeStatusAsync(int id, PrintControlChangeStatusRequest request);

        Task<PrintControlResponse> InsertAsync(PrintControlInsertRequest request);

        Task<PaginationResponse<PrintControlResponse>> ListAsync(PrintControlListRequest request);

        Task<PrintControlResponse> GetLastAsync();

        Task<PrintControlResponse> ValidateAsync(int id);
    }
}
