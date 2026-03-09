using AutoMapper;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.Domain.Utils.Models;

namespace ReiDoChopp.Application.Utils.Profiles
{
    public class PaginationProfile : Profile
    {
        public PaginationProfile()
        {
            CreateMap(typeof(PaginationModel<>), typeof(PaginationResponse<>));
        }
    }
}
