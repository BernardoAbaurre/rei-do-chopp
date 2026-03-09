using AutoMapper;
using ReiDoChopp.DataTransfer.RestockingProducts.Requests;
using ReiDoChopp.DataTransfer.RestockingProducts.Response;
using ReiDoChopp.Domain.RestockingProducts.Entities;
using ReiDoChopp.Domain.RestockingProducts.Repositories.Filters;
using ReiDoChopp.Domain.RestockingProducts.Services.Commands;

namespace ReiDoChopp.Application.RestockingProducts.Profiles
{
    public class RestockingProductsProfile : Profile
    {
        public RestockingProductsProfile()
        {
            CreateMap<RestockingProduct, RestockingProductResponse>();

            CreateMap<RestockingProduct, RestockingProductHistoryResponse>();

            CreateMap<RestockingProductListRequest, RestockingProductsListFilter>();

            CreateMap<RestockingProductRequest, RestockingProductCommand>();
        }
    }
}
