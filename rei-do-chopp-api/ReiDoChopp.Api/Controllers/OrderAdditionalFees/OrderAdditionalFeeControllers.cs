using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReiDoChopp.Application.OrderAdditionalFees.Services.Interfaces;
using ReiDoChopp.DataTransfer.OrderAdditionalFees.Request;
using ReiDoChopp.DataTransfer.OrderAdditionalFees.Response;
using ReiDoChopp.DataTransfer.Pagination.Responses;

namespace ReiDoChopp.API.Controllers.OrderAdditionalFees
{
    [ApiController]
    [Authorize]
    [Route("api/orders-additional-fees")]
    public class OrderAdditionalFeesController : ControllerBase
    {
        private readonly IOrderAdditionalFeesAppService orderAdditionalFeesAppService;

        public OrderAdditionalFeesController(IOrderAdditionalFeesAppService orderAdditionalFeesAppService)
        {
            this.orderAdditionalFeesAppService = orderAdditionalFeesAppService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderAdditionalFeeResponse>> GetAsync(int id)
        {
            var response = await orderAdditionalFeesAppService.ValidateAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<OrderAdditionalFeeResponse>>> ListAsync([FromQuery] OrderAdditionalFeeListRequest request)
        {
            var response = await orderAdditionalFeesAppService.ListAsync(request);
            return Ok(response);
        }

    }
}
