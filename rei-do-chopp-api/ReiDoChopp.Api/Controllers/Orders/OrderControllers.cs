using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReiDoChopp.Application.Orders.Services.Interfaces;
using ReiDoChopp.Application.Restockings.Services;
using ReiDoChopp.DataTransfer.Orders.Request;
using ReiDoChopp.DataTransfer.Orders.Response;
using ReiDoChopp.DataTransfer.Orders.Responses;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.PrintControls.Response;

namespace ReiDoChopp.API.Controllers.Orders
{
    [ApiController]
    [Authorize]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersAppService ordersAppService;

        public OrdersController(IOrdersAppService ordersAppService)
        {
            this.ordersAppService = ordersAppService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetAsync(int id)
        {
            OrderResponse response = await ordersAppService.ValidateAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<OrderHistoryResponse>>> ListAsync([FromQuery] OrderListRequest request)
        {
            PaginationResponse<OrderHistoryResponse> response = await ordersAppService.ListAsync(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponse>> InsertAsync([FromBody] OrderRequest request)
        {
            OrderResponse response = await ordersAppService.InsertAsync(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderResponse>> EditAsync(int id, [FromBody] OrderRequest request)
        {
            OrderResponse response = await ordersAppService.EditAsync(id, request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await ordersAppService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("totals-calculations")]
        public ActionResult<OrderTotalsCalculationResponse> CalculateTotals([FromBody] OrderRequest request)
        {
            OrderTotalsCalculationResponse response = ordersAppService.CalculateTotals(request);
            return Ok(response);
        }

        [HttpPost("prints/{orderId}")]
        public async Task<ActionResult<PrintControlResponse>> Print(int orderId)
        {
            PrintControlResponse response = await ordersAppService.PrintAsync(orderId);
            return Ok(response);
        }
    }
}
