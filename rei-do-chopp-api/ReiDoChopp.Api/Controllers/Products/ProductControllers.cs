using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReiDoChopp.Application.Products.Services.Interfaces;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Products.Request;
using ReiDoChopp.DataTransfer.Products.Response;

namespace ReiDoChopp.API.Controllers.Products
{
    [ApiController]
    [Authorize]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsAppService productsAppService;

        public ProductsController(IProductsAppService productsAppService)
        {
            this.productsAppService = productsAppService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductResponse>> GetAsync(int id)
        {
            ProductResponse response = await productsAppService.ValidateAsync(id);

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginationResponse<ProductResponse>>> ListAsync([FromQuery] ProductListRequest request)
        {
            PaginationResponse<ProductResponse> response = await productsAppService.ListAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductResponse>> InsertAsync([FromBody] ProductInsertRequest request)
        {
            ProductResponse response = await productsAppService.InsertAsync(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductResponse>> EditAsync(int id, [FromBody] ProductEditRequest request)
        {
            ProductResponse response = await productsAppService.EditAsync(id, request);
            return Ok(response);
        }

        [HttpPut("status-changes/{id}")]
        [Authorize]
        public async Task<ActionResult<ProductResponse>> ChangeStatusAsync(int id)
        {
            ProductResponse response = await productsAppService.ChangeStatusAsync(id);
            return Ok(response);
        }
    }
}
