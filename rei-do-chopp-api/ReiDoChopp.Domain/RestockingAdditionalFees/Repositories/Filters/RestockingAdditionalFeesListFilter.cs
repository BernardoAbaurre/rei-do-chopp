using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.Domain.Utils.Filters;

namespace ReiDoChopp.Domain.RestockingAdditionalFees.Repositories.Filters
{
    public class RestockingAdditionalFeesListFilter : PaginationFilter
    {
        public int? Id { get; set; }
        public int? RestockingId { get; set; }
        public double? Value { get; set; }
        public string Description { get; set; }


        public RestockingAdditionalFeesListFilter() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) {}
    }
}