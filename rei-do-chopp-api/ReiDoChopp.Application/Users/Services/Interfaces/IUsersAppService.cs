using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Users.Requests;
using ReiDoChopp.DataTransfer.Users.Responses;

namespace ReiDoChopp.Application.Users.Services.Interfaces
{
    public interface IUsersAppService
    {
        Task<LoginResponse> LoginAsync(UserLoginRequest request);
        Task<UserResponse> RegisterAsync(UserRegisterRequest request);
        Task<UserResponse> ValidateAsync(int userId);
        Task ForgotPasswordAsync(UserForgotPasswordRequest request);
        Task ResetPasswordAsync(UserResetPasswordRequest request);
        Task<UserResponse> GetCurrentUserAsync();
        Task<PaginationResponse<UserResponse>> ListAsync(UserListRequest request);
        Task<UserResponse> ChangeStatusAsync(int id);
        Task<DbTestResponse> DbTest();

        Task<UserResponse> EditAsync(int id, UserEditRequest request);
    }
}
