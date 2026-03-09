
using ReiDoChopp.Domain.Utils;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.Domain.Utils.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using ReiDoChopp.Infra.Data;
using ReiDoChopp.Domain.Utils.Filters;

namespace ReiDoChopp.Infra.Utils
{
    public class EntityFrameworkRepository<T> : IEntityFrameworkRepository<T> where T : class
    {
        protected readonly ReiDoChoppDbContext dbContext;
        protected readonly DbSet<T> dbSet;

        public EntityFrameworkRepository(ReiDoChoppDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Edit(T entity)
        {
            dbSet.Update(entity);
        }

        public IQueryable<T> Query()
        {
            return dbSet.AsQueryable();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task InsertAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<PaginationModel<T>> ListAsync(IQueryable<T> query, PaginationFilter filter)
        {
            return await ListAsync(query, filter.Page, filter.PageSize, filter.OrdenationField, filter.OrdenationType);
        }

        public async Task<PaginationModel<T>> ListAsync(IQueryable<T> query, int page, int pageSize, string ordenationField, OrdenationTypeEnum ordenationType)
        {
            var propertyInfo = typeof(T).GetProperty(ordenationField);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Field '{ordenationField}' not exists in '{typeof(T).Name}'.");
            }

            if (ordenationType == OrdenationTypeEnum.ASC)
            {
                query = query.OrderBy(ordenationField);
            }
            else
            {
                query = query.OrderBy(ordenationField + " descending");
            }

            var results = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var count = query.Count();

            return new PaginationModel<T>
            {
                Total = count,
                Page = page,
                PageCount = (int)Math.Ceiling((double)count / pageSize),
                Data = results
            };
        } 
    }
}
