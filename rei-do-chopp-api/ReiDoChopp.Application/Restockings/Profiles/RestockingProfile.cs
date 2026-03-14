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

            CreateMap<RestockingListRequest, RestockingsListFilter>()
                .ForMember(dest => dest.InitialDate, opt => opt.MapFrom(src => src.InitialDate.HasValue ? DateTime.SpecifyKind(src.InitialDate.Value, DateTimeKind.Utc) : (DateTime?)null))
                .ForMember(dest => dest.FinalDate, opt => opt.MapFrom(src => src.FinalDate.HasValue ? DateTime.SpecifyKind(src.FinalDate.Value, DateTimeKind.Utc) : (DateTime?)null));

            CreateMap<RestockingRequest, RestockingCommand>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Date, DateTimeKind.Utc)));

            CreateMap<RestockingHistoryModel, RestockingHistoryResponse>();

            CreateMap<PaginationModel<RestockingHistoryModel>, PaginationResponse<RestockingHistoryResponse>>();

            CreateMap<RestockingTotalsCalculationModel, RestockingTotalsCalculationResponse>();
        }
    }
}
