using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReiDoChopp.Application.RestockingAdditionalFees.Services.Interfaces;
using ReiDoChopp.DataTransfer.RestockingAdditionalFees.Requests;
using ReiDoChopp.DataTransfer.RestockingAdditionalFees.Response;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using Microsoft.AspNetCore.Authorization;

namespace ReiDoChopp.API.Controllers.RestockingAdditionalFees
{
    [ApiController]
    [Authorize]
    [Route("api/restocking-additional-fees")]
    public class RestockingAdditionalFeesController : ControllerBase
    {
        private readonly IRestockingAdditionalFeesAppService restockingAdditionalFeesAppService;

        public RestockingAdditionalFeesController(IRestockingAdditionalFeesAppService restockingAdditionalFeesAppService)
        {
            this.restockingAdditionalFeesAppService = restockingAdditionalFeesAppService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestockingAdditionalFeeResponse>> GetAsync(int id)
        {
            var response = await restockingAdditionalFeesAppService.ValidateAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<RestockingAdditionalFeeResponse>>> ListAsync([FromQuery] RestockingAdditionalFeeListRequest request)
        {
            var response = await restockingAdditionalFeesAppService.ListAsync(request);
            return Ok(response);
        }
    }
}
