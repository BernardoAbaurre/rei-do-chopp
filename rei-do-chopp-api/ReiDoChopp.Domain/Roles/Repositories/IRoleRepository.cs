using System.Linq;
using ReiDoChopp.Domain.Roles.Entities;
using ReiDoChopp.Domain.Roles.Repositories.Filters;
using ReiDoChopp.Domain.Utils;
using ReiDoChopp.Domain.Utils.Models;

namespace ReiDoChopp.Domain.Roles.Repositories
{
    public interface IRolesRepository : IEntityFrameworkRepository<Role>
    {
        IQueryable<Role> Filter(RolesListFilter filter);
    }
}
