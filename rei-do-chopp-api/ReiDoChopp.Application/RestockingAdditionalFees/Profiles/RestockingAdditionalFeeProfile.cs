using AutoMapper;
using ReiDoChopp.DataTransfer.RestockingAdditionalFees.Requests;
using ReiDoChopp.DataTransfer.RestockingAdditionalFees.Response;
using ReiDoChopp.Domain.RestockingAdditionalFees.Entities;
using ReiDoChopp.Domain.RestockingAdditionalFees.Repositories.Filters;
using ReiDoChopp.Domain.RestockingAdditionalFees.Services.Command;

namespace ReiDoChopp.Application.RestockingAdditionalFees.Profiles
{
    public class RestockingAdditionalFeesProfile : Profile
    {
        public RestockingAdditionalFeesProfile()
        {
            CreateMap<RestockingAdditionalFee, RestockingAdditionalFeeResponse>();

            CreateMap<RestockingAdditionalFeeListRequest, RestockingAdditionalFeesListFilter>();

            CreateMap<RestockingAdditionalFeeRequest, RestockingAdditionalFeeCommand>();
        }
    }
}
