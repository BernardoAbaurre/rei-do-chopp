using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReiDoChopp.Application.Roles.Services.Interfaces;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Roles.Requests;
using ReiDoChopp.DataTransfer.Roles.Responses;

namespace ReiDoChopp.API.Controllers.Roles
{
    [ApiController]
    [Authorize]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesAppService rolesAppService;

        public RolesController(IRolesAppService rolesAppService)
        {
            this.rolesAppService = rolesAppService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleResponse>> GetAsync(int id)
        {
            var response = await rolesAppService.ValidateAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<RoleResponse>>> ListAsync([FromQuery] RoleListRequest request)
        {
            var response = await rolesAppService.ListAsync(request);
            return Ok(response);
        }
    }
}
