using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReiDoChopp.Application.OpertaionsHistories.Services.Interfaces;
using ReiDoChopp.DataTransfer.OpertaionsHistories.Requests;
using ReiDoChopp.DataTransfer.OpertaionsHistories.Response;
using ReiDoChopp.DataTransfer.Pagination.Responses;

namespace ReiDoChopp.API.Controllers.OpertaionsHistories
{
    [ApiController]
    [Authorize]
    [Route("api/operations-histories")]
    public class OpertaionsHistoriesController : ControllerBase
    {
        private readonly IOpertaionsHistoriesAppService opertaionsHistoriesAppService;

        public OpertaionsHistoriesController(IOpertaionsHistoriesAppService opertaionsHistoriesAppService)
        {
            this.opertaionsHistoriesAppService = opertaionsHistoriesAppService;
        }

        [HttpGet]
        public async Task<ActionResult<OperationHistoryResponse>> ListAsync([FromQuery] OperationHistoryListRequest request)
        {
            var response = await opertaionsHistoriesAppService.ListAsync(request);
            return Ok(response);
        }
    }
}
