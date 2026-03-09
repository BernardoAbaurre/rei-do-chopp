using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReiDoChopp.Domain.Roles.Entities;
using ReiDoChopp.Domain.Users.Services.Interfaces;
using ReiDoChopp.Domain.Utils.Exceptions;
using System.Data;

namespace ReiDoChopp.Domain.Roles.Services
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<Role> roleManager;
        private readonly IUsersService usersService;

        public RolesService(RoleManager<Role> roleManager, IUsersService usersService)
        {
            this.roleManager = roleManager;
            this.usersService = usersService;
        }

        public async Task<Role> ValidateAsync(int roleId)
        {
            Role user = await roleManager.FindByIdAsync(roleId.ToString());

            if (user == null)
            {
                throw new RegisterNotFound(roleId);
            }

            return user;
        }

        public async Task<IEnumerable<Role>> GetRolesByIdsAsync(int[] roleIds)
        {
            var roles = roleManager.Roles;

            if(roleIds.Length > 0)
            {
                roles = roles.Where(x => roleIds.Contains(x.Id));
            }

            return await roles.ToArrayAsync();
        }
    }
}
