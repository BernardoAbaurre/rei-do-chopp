using AutoMapper;
using ReiDoChopp.DataTransfer.OpertaionsHistories.Requests;
using ReiDoChopp.DataTransfer.OpertaionsHistories.Response;
using ReiDoChopp.Domain.OpertaionsHistories.Repositories.Filters;
using ReiDoChopp.Domain.OpertaionsHistories.Repositories.Models;

namespace ReiDoChopp.Application.OpertaionsHistories.Profiles
{
    public class OpertaionsHistoriesProfile : Profile
    {
        public OpertaionsHistoriesProfile()
        {
            CreateMap<OperationHistoryModel, OperationHistoryResponse>();

            CreateMap<OperationHistoryListRequest, OpertaionsHistoriesListFilter>()
                .ForMember(dest => dest.InitialDate, opt => opt.MapFrom(src => src.InitialDate.HasValue ? DateTime.SpecifyKind(src.InitialDate.Value, DateTimeKind.Utc) : (DateTime?)null))
                .ForMember(dest => dest.FinalDate, opt => opt.MapFrom(src => src.FinalDate.HasValue ? DateTime.SpecifyKind(src.FinalDate.Value, DateTimeKind.Utc) : (DateTime?)null));
        }
    }
}
