using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.Domain.Utils.Models;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReiDoChopp.Domain.Utils.Filters;

namespace ReiDoChopp.Domain.Utils.Extensions
{
    public static class PaginationExtensions
    {
        public static async Task<PaginationModel<T>> PageAsync<T>(this IQueryable<T> query, int page, int pageSize, string ordenationField, OrdenationTypeEnum ordenationType) where T : class
        { 
            var count = query.Count();

            if (ordenationType == OrdenationTypeEnum.ASC)
            {
                query = query.OrderBy(ordenationField);
            }
            else
            {
                query = query.OrderBy(ordenationField + " descending");
            }

            var results = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();


            return new PaginationModel<T>
            {
                Total = count,
                Page = page,
                PageCount = (int)Math.Ceiling((double)count / pageSize),
                Data = results
            };
        }

        public static async Task<PaginationModel<T>> PageAsync<T>(this IQueryable<T> query, PaginationFilter filter) where T : class
        {
            return await query.PageAsync(filter.Page, filter.PageSize, filter.OrdenationField, filter.OrdenationType);
        }
    }
}
