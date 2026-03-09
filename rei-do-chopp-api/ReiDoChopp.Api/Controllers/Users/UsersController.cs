using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReiDoChopp.Application.Users.Services.Interfaces;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Users.Requests;
using ReiDoChopp.DataTransfer.Users.Responses;

namespace ReiDoChopp.Api.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersAppService usersAppService;

        public UsersController(IUsersAppService usersAppService)
        {
            this.usersAppService = usersAppService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> ValidateAsync(int id)
        {
            UserResponse response = await usersAppService.ValidateAsync(id);

            return Ok(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> AddRoleToUserAsync(int id, [FromBody] UserEditRequest request)
        {
            UserResponse response = await usersAppService.EditAsync(id, request);

            return Ok(response);
        }

        [HttpPost("logins")]
        public async Task<ActionResult<LoginResponse>> LoginAsync([FromBody] UserLoginRequest request)
        {
            LoginResponse response = await usersAppService.LoginAsync(request);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("registers")]
        public async Task<ActionResult<UserResponse>> RegisterAsync([FromBody] UserRegisterRequest request)
        {
            UserResponse response = await usersAppService.RegisterAsync(request);

            return Ok(response);
        }

        [HttpPost("forgotten-passwords")]
        public async Task<ActionResult> ForgotPasswordAsync(UserForgotPasswordRequest request)
        {
            await usersAppService.ForgotPasswordAsync(request);

            return Ok();
        }

        [HttpPost("passwords-resets")]
        public async Task<ActionResult> ResetPassword([FromBody] UserResetPasswordRequest request)
        {
            await usersAppService.ResetPasswordAsync(request);

            return Ok();
        }

        [HttpGet("current")]
        [Authorize]
        public async Task<ActionResult<UserResponse>> CheckAuthAsync()
        {
            return await usersAppService.GetCurrentUserAsync();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginationResponse<UserResponse>>> ListAsync([FromQuery] UserListRequest request)
        {
            return await usersAppService.ListAsync(request);
        }

        [HttpPut("status-changes/{id}")]
        [Authorize]
        public async Task<ActionResult<UserResponse>> ChangeStatusAsync(int id)
        {
            UserResponse response = await usersAppService.ChangeStatusAsync(id);
            return Ok(response);
        }

        [HttpGet("db-tests")]
        public async Task<ActionResult<DbTestResponse>> DbTests()
        {
            DbTestResponse response = await usersAppService.DbTest();
            return Ok(response);
        }
    }
}
