using AutoMapper;
using ReiDoChopp.DataTransfer.OrdersProducts.Request;
using ReiDoChopp.DataTransfer.OrdersProducts.Response;
using ReiDoChopp.Domain.OrdersProducts.Entities;
using ReiDoChopp.Domain.OrdersProducts.Repositories.Filters;
using ReiDoChopp.Domain.OrdersProducts.Services.Command;

namespace ReiDoChopp.Application.OrdersProducts.Profiles
{
    public class OrdersProductsProfile : Profile
    {
        public OrdersProductsProfile()
        {
            CreateMap<OrderProduct, OrderProductResponse>();

            CreateMap<OrderProduct, OrderProductHistoryResponse>();

            CreateMap<OrderProductListRequest, OrdersProductsListFilter>();

            CreateMap<OrderProductRequest, OrderProductCommand>();
        }
    }
}
