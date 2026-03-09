using System.Threading.Tasks;
using ReiDoChopp.DataTransfer.Products.Request;
using ReiDoChopp.DataTransfer.Products.Response;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.Domain.Products.Entities;

namespace ReiDoChopp.Application.Products.Services.Interfaces
{
    public interface IProductsAppService
    {
        Task<ProductResponse> EditAsync(int id, ProductEditRequest request);

        Task<ProductResponse> InsertAsync(ProductInsertRequest request);

        Task<PaginationResponse<ProductResponse>> ListAsync(ProductListRequest request);

        Task<ProductResponse> ValidateAsync(int id);

        Task<ProductResponse> ChangeStatusAsync(int id);
    }
}
