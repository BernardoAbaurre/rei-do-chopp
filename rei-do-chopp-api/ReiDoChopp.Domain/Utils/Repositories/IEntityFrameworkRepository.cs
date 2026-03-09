
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.Domain.Utils.Filters;
using ReiDoChopp.Domain.Utils.Models;

namespace ReiDoChopp.Domain.Utils
{
    public interface IEntityFrameworkRepository<T> where T : class
    {
        Task<T?> FindByIdAsync(int id);
        Task<PaginationModel<T>> ListAsync(IQueryable<T> query, int page, int pageSize, string ordenationField, OrdenationTypeEnum ordenationType);
        Task<PaginationModel<T>> ListAsync(IQueryable<T> query, PaginationFilter filter);
        void Edit(T entity);
        Task InsertAsync(T entity);
        void Delete(T entity);
        IQueryable<T> Query();
    }
}
