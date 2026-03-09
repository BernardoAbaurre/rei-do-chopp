using AutoMapper;
using ReiDoChopp.DataTransfer.Orders.Request;
using ReiDoChopp.DataTransfer.Orders.Response;
using ReiDoChopp.DataTransfer.Orders.Responses;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.Domain.Orders.Entities;
using ReiDoChopp.Domain.Orders.Repositories.Filters;
using ReiDoChopp.Domain.Orders.Repositories.Models;
using ReiDoChopp.Domain.Orders.Services.Commands;
using ReiDoChopp.Domain.Utils.Models;

namespace ReiDoChopp.Application.Orders.Profiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.AttendantName, opt => opt.MapFrom(src => src.Attendant.FirstName + " " + src.Attendant.LastName))
                .ForMember(dest => dest.CashierName, opt => opt.MapFrom(src => src.Cashier.FirstName + " " + src.Cashier.LastName));
             
            CreateMap<OrderListRequest, OrdersListFilter>();

            CreateMap<OrderRequest, OrderCommand>();

            CreateMap<OrderHistoryModel, OrderHistoryResponse>();

            CreateMap<PaginationModel<OrderHistoryModel>, PaginationResponse<OrderHistoryResponse>>();

            CreateMap<OrderTotalsCalculationModel, OrderTotalsCalculationResponse>();
        }
    }
}
