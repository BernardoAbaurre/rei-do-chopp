using AutoMapper;
using ReiDoChopp.DataTransfer.Roles.Requests;
using ReiDoChopp.DataTransfer.Roles.Responses;
using ReiDoChopp.Domain.Roles.Entities;
using ReiDoChopp.Domain.Roles.Repositories.Filters;
using ReiDoChopp.Domain.Roles.Repositories.Models;

namespace ReiDoChopp.Application.Roles.Profiles
{
    public class RolesProfile : Profile
    {
        public RolesProfile()
        {
            CreateMap<Role, RoleResponse>();
            CreateMap<RoleModel, RoleResponse>();

            CreateMap<RoleListRequest, RolesListFilter>();
        }
    }
}
