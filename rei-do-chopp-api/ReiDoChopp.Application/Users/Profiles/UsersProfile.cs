using AutoMapper;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Users.Requests;
using ReiDoChopp.DataTransfer.Users.Responses;
using ReiDoChopp.Domain.Users.Entities;
using ReiDoChopp.Domain.Users.Repositories.Filters;
using ReiDoChopp.Domain.Users.Repositories.Models;
using ReiDoChopp.Domain.Utils.Models;

namespace ReiDoChopp.Application.Users.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(r => r.Role)));
            CreateMap<UserListRequest, UsersListFilter>();
            CreateMap<PaginationModel<UserModel>, PaginationResponse<UserResponse>>();
            CreateMap<UserModel, UserResponse>();
        }
    }
}
