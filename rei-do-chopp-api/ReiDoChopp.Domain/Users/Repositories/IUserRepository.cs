using ReiDoChopp.Domain.Users.Entities;
using ReiDoChopp.Domain.Users.Repositories.Filters;
using ReiDoChopp.Domain.Users.Repositories.Models;
using ReiDoChopp.Domain.Utils;
using ReiDoChopp.Domain.Utils.Models;

namespace ReiDoChopp.Domain.Users.Repositories
{
    public interface IUsersRepository : IEntityFrameworkRepository<User>
    {
        IQueryable<User> Filter(UsersListFilter filter);
        Task<PaginationModel<UserModel>> ListProjectionAsync(IQueryable<User> query, UsersListFilter filter);
    }
}
