using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.Domain.Utils.Filters;

namespace ReiDoChopp.Domain.Roles.Repositories.Filters
{
    public class RolesListFilter : PaginationFilter
    {
        public int? Id { get; set; }
        public string Name { get; set; }


        public RolesListFilter() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) {}
    }
}