using AutoMapper;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Restockings.Request;
using ReiDoChopp.DataTransfer.Restockings.Response;
using ReiDoChopp.DataTransfer.Restockings.Responses;
using ReiDoChopp.Domain.Orders.Repositories.Models;
using ReiDoChopp.Domain.Restockings.Entities;
using ReiDoChopp.Domain.Restockings.Repositories.Filters;
using ReiDoChopp.Domain.Restockings.Repositories.Models;
using ReiDoChopp.Domain.Restockings.Services.Commands;
using ReiDoChopp.Domain.Utils.Models;

namespace ReiDoChopp.Application.Restockings.Profiles
{
    public class RestockingsProfile : Profile
    {
        public RestockingsProfile()
        {
            CreateMap<Restocking, RestockingResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

            CreateMap<RestockingListRequest, RestockingsListFilter>();

            CreateMap<RestockingRequest, RestockingCommand>();

            CreateMap<RestockingHistoryModel, RestockingHistoryResponse>();

            CreateMap<PaginationModel<RestockingHistoryModel>, PaginationResponse<RestockingHistoryResponse>>();

            CreateMap<RestockingTotalsCalculationModel, RestockingTotalsCalculationResponse>();
        }
    }
}
