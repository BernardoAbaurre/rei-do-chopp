using Microsoft.AspNetCore.Identity;
using ReiDoChopp.Domain.Roles.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReiDoChopp.Domain.Roles.Services
{
    public interface IRolesService
    {
        Task<Role> ValidateAsync(int roleId);
        Task<IEnumerable<Role>> GetRolesByIdsAsync(int[] roleIds);
    }
}
