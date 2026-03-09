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

            CreateMap<OperationHistoryListRequest, OpertaionsHistoriesListFilter>();
        }
    }
}
