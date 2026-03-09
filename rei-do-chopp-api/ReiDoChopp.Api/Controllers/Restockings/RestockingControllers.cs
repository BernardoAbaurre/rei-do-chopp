using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReiDoChopp.Application.Restockings.Services.Interfaces;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Restockings.Request;
using ReiDoChopp.DataTransfer.Restockings.Response;
using ReiDoChopp.DataTransfer.Restockings.Responses;

namespace ReiDoChopp.API.Controllers.Restockings
{
    [ApiController]
    [Authorize]
    [Route("api/restockings")]
    public class RestockingsController : ControllerBase
    {
        private readonly IRestockingsAppService restockingsAppService;

        public RestockingsController(IRestockingsAppService restockingsAppService)
        {
            this.restockingsAppService = restockingsAppService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestockingResponse>> GetAsync(int id)
        {
            RestockingResponse response = await restockingsAppService.ValidateAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<RestockingHistoryResponse>>> ListAsync([FromQuery] RestockingListRequest request)
        {
            PaginationResponse<RestockingHistoryResponse> response = await restockingsAppService.ListAsync(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<RestockingResponse>> InsertAsync([FromBody] RestockingRequest request)
        {
            RestockingResponse response = await restockingsAppService.InsertAsync(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RestockingResponse>> EditAsync(int id, [FromBody] RestockingRequest request)
        {
            RestockingResponse response = await restockingsAppService.EditAsync(id, request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await restockingsAppService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("totals-calculations")]
        public ActionResult<RestockingTotalsCalculationResponse> CalculateTotals([FromBody] RestockingRequest request)
        {
            RestockingTotalsCalculationResponse response = restockingsAppService.CalculateTotals(request);
            return Ok(response);
        }
    }
}
