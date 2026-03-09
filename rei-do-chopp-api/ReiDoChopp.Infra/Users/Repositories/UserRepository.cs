using ReiDoChopp.Domain.Roles.Repositories.Models;
using ReiDoChopp.Domain.Users.Entities;
using ReiDoChopp.Domain.Users.Repositories;
using ReiDoChopp.Domain.Users.Repositories.Filters;
using ReiDoChopp.Domain.Users.Repositories.Models;
using ReiDoChopp.Domain.Utils.Extensions;
using ReiDoChopp.Domain.Utils.Models;
using ReiDoChopp.Infra.Data;
using ReiDoChopp.Infra.Utils;

namespace ReiDoChopp.Infra.Users.Repositories
{
    public class UsersRepository : EntityFrameworkRepository<User>, IUsersRepository
    {
        public UsersRepository(ReiDoChoppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<User> Filter(UsersListFilter filter)
        {
            IQueryable<User> query = Query();

            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => (x.FirstName.ToUpper() + " " + x.LastName.ToUpper()).Contains(filter.Name.ToUpper()));
            }

            if (filter.Active.HasValue)
            {
                query = query.Where(x => x.Active == filter.Active.Value);
            }

            if (filter.RoleIds.Length > 0)
            {
                query = query.Where(x => x.Roles.Any(r => filter.RoleIds.Contains(r.RoleId)));
            }

            return query;
        }

        public async Task<PaginationModel<UserModel>> ListProjectionAsync(IQueryable<User> query, UsersListFilter filter)
        {
            return await query.Select(x => new UserModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                FullName = x.FirstName + " " + x.LastName,
                Active = x.Active,
                CreationDate = x.CreationDate,
                Email = x.Email,
                Roles = x.Roles.Select(r => new RoleModel { Id = r.RoleId, Name = r.Role.Name})
            }).PageAsync(filter);
        }
    }
}
