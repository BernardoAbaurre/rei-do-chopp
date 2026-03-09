using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReiDoChopp.Domain.Roles.Entities;
using ReiDoChopp.Domain.Roles.Repositories;
using ReiDoChopp.Domain.Roles.Repositories.Filters;
using ReiDoChopp.Infra.Utils;
using ReiDoChopp.Infra.Data;

namespace ReiDoChopp.Infra.Roles.Repositories
{
    public class RolesRepository : EntityFrameworkRepository<Role>, IRolesRepository
    {
        public RolesRepository(ReiDoChoppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Role> Filter(RolesListFilter filter)
        {
            var query = Query();
            
            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.Name.ToUpper().Contains(filter.Name.ToUpper()));
            }

            return query;
        }
    }
}
