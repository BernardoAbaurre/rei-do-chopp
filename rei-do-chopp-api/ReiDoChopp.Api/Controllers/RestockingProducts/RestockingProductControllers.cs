using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReiDoChopp.Application.RestockingProducts.Services.Interfaces;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.RestockingProducts.Requests;
using ReiDoChopp.DataTransfer.RestockingProducts.Response;

namespace ReiDoChopp.API.Controllers.RestockingProducts
{
    [ApiController]
    [Authorize]
    [Route("api/restocking-products")]
    public class RestockingProductsController : ControllerBase
    {
        private readonly IRestockingProductsAppService restockingProductsAppService;

        public RestockingProductsController(IRestockingProductsAppService restockingProductsAppService)
        {
            this.restockingProductsAppService = restockingProductsAppService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestockingProductResponse>> GetAsync(int id)
        {
            RestockingProductResponse response = await restockingProductsAppService.ValidateAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<RestockingProductHistoryResponse>>> ListAsync([FromQuery] RestockingProductListRequest request)
        {
            PaginationResponse<RestockingProductHistoryResponse> response = await restockingProductsAppService.ListAsync(request);
            return Ok(response);
        }
    }
}
