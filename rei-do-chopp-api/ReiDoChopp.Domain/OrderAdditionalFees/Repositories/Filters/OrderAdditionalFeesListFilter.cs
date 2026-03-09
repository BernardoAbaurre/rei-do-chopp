using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.Domain.Utils.Filters;

namespace ReiDoChopp.Domain.OrderAdditionalFees.Repositories.Filters
{
    public class OrderAdditionalFeesListFilter : PaginationFilter
    {
        public int? Id { get; set; }
        public int? OrderId { get; set; }
        public double? Value { get; set; }
        public string Description { get; set; }


        public OrderAdditionalFeesListFilter() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) {}
    }
}