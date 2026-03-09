using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Roles.Responses;
using ReiDoChopp.DataTransfer.Roles.Requests;

namespace ReiDoChopp.Application.Roles.Services.Interfaces
{
    public interface IRolesAppService
    {
        Task<PaginationResponse<RoleResponse>> ListAsync(RoleListRequest request);

        Task<RoleResponse> ValidateAsync(int id);
    }
}
