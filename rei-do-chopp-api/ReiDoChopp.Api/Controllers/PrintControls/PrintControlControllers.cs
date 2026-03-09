using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReiDoChopp.Application.PrintControls.Services.Interfaces;
using ReiDoChopp.DataTransfer.PrintControls.Requests;
using ReiDoChopp.DataTransfer.PrintControls.Response;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using Microsoft.AspNetCore.Authorization;

namespace ReiDoChopp.API.Controllers.PrintControls
{
    [ApiController]
    [Route("api/print-controls")]
    public class PrintControlsController : ControllerBase
    {
        private readonly IPrintControlsAppService printControlsAppService;

        public PrintControlsController(IPrintControlsAppService printControlsAppService)
        {
            this.printControlsAppService = printControlsAppService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrintControlResponse>> GetAsync(int id)
        {
            var response = await printControlsAppService.ValidateAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<PrintControlResponse>>> ListAsync([FromQuery] PrintControlListRequest request)
        {
            var response = await printControlsAppService.ListAsync(request);
            return Ok(response);
        }

        [HttpGet("last")]
        public async Task<ActionResult<PaginationResponse<PrintControlResponse>>> GetLastAsync()
        {
            var response = await printControlsAppService.GetLastAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<PrintControlResponse>> InsertAsync([FromBody] PrintControlInsertRequest request)
        {
            var response = await printControlsAppService.InsertAsync(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PrintControlResponse>> ChangeStatusAsync(int id, [FromBody] PrintControlChangeStatusRequest request)
        {
            var response = await printControlsAppService.ChangeStatusAsync(id, request);
            return Ok(response);
        }
    }
}
