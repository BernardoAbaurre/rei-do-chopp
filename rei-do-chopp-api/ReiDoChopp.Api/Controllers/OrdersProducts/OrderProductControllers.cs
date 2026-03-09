using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReiDoChopp.Application.OrdersProducts.Services.Interfaces;
using ReiDoChopp.DataTransfer.OrdersProducts.Request;
using ReiDoChopp.DataTransfer.OrdersProducts.Response;
using ReiDoChopp.DataTransfer.Pagination.Responses;

namespace ReiDoChopp.API.Controllers.OrdersProducts
{
    [ApiController]
    [Authorize]
    [Route("api/orders-products")]
    public class OrdersProductsController : ControllerBase
    {
        private readonly IOrdersProductsAppService ordersProductsAppService;

        public OrdersProductsController(IOrdersProductsAppService ordersProductsAppService)
        {
            this.ordersProductsAppService = ordersProductsAppService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProductResponse>> GetAsync(int id)
        {
            var response = await ordersProductsAppService.ValidateAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<OrderProductHistoryResponse>>> ListAsync([FromQuery] OrderProductListRequest request)
        {
            var response = await ordersProductsAppService.ListAsync(request);
            return Ok(response);
        }
    }
}
