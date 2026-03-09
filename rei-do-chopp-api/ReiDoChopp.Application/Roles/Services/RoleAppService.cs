using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using ReiDoChopp.Application.Roles.Services.Interfaces;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Roles.Requests;
using ReiDoChopp.DataTransfer.Roles.Responses;
using ReiDoChopp.Domain.Roles.Entities;
using ReiDoChopp.Domain.Roles.Repositories;
using ReiDoChopp.Domain.Roles.Repositories.Filters;
using ReiDoChopp.Domain.Roles.Services;
using ReiDoChopp.Domain.Utils.Models;
using ReiDoChopp.Infra.Data;

namespace ReiDoChopp.Application.Roles.Services
{
    public class RolesAppService : IRolesAppService
    {
        private readonly IMapper mapper;
        private readonly ReiDoChoppDbContext dbContext;
        private readonly IRolesRepository rolesRepository;
        private readonly IRolesService rolesService;

        public RolesAppService(
            IMapper mapper,
            ReiDoChoppDbContext dbContext,
            IRolesRepository rolesRepository,
            IRolesService rolesService)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.rolesRepository = rolesRepository;
            this.rolesService = rolesService;
        }

        public async Task<PaginationResponse<RoleResponse>> ListAsync(RoleListRequest request)
        {
            RolesListFilter filter = mapper.Map<RolesListFilter>(request);

            IQueryable<Role> query = rolesRepository.Filter(filter);

            PaginationModel<Role> response = await rolesRepository.ListAsync(query, filter);

            return mapper.Map<PaginationResponse<RoleResponse>>(response);
        }

        public async Task<RoleResponse> ValidateAsync(int id)
        {
            Role entity = await rolesService.ValidateAsync(id);
            return mapper.Map<RoleResponse>(entity);
        }
    }
}
