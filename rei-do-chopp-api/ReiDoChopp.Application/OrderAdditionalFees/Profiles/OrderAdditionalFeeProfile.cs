using AutoMapper;
using ReiDoChopp.DataTransfer.OrderAdditionalFees.Request;
using ReiDoChopp.DataTransfer.OrderAdditionalFees.Response;
using ReiDoChopp.Domain.OrderAdditionalFees.Entities;
using ReiDoChopp.Domain.OrderAdditionalFees.Repositories.Filters;
using ReiDoChopp.Domain.OrderAdditionalFees.Services.Commands;

namespace ReiDoChopp.Application.OrderAdditionalFees.Profiles
{
    public class OrderAdditionalFeesProfile : Profile
    {
        public OrderAdditionalFeesProfile()
        {
            CreateMap<OrderAdditionalFee, OrderAdditionalFeeResponse>();

            CreateMap<OrderAdditionalFeeListRequest, OrderAdditionalFeesListFilter>();

            CreateMap<OrderAdditionalFeeRequest, OrderAdditionalFeeCommand>();
        }
    }
}
