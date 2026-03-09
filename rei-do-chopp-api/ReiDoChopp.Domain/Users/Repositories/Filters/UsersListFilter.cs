using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.Domain.Utils.Filters;

namespace ReiDoChopp.Domain.Users.Repositories.Filters
{
    public class UsersListFilter : PaginationFilter
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public int[] RoleIds { get; set; }

        public UsersListFilter() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) {}
    }
}