using AutoMapper;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Products.Request;
using ReiDoChopp.DataTransfer.Products.Response;
using ReiDoChopp.Domain.Products.Entities;
using ReiDoChopp.Domain.Products.Repositories.Filters;
using ReiDoChopp.Domain.Products.Repositories.Models;
using ReiDoChopp.Domain.Utils.Models;

namespace ReiDoChopp.Application.Products.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Alert, opt => opt.MapFrom(src => src.StockQuantity <= src.AlertQuantity));

            CreateMap<ProductListRequest, ProductsListFilter>();

            CreateMap<ProductModel, ProductResponse>();

            CreateMap<PaginationModel<ProductModel>, PaginationResponse<ProductResponse>>();
        }
    }
}
