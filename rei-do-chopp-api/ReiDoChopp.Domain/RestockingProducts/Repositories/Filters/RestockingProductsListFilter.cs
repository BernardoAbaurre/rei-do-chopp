using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.Domain.Utils.Filters;

namespace ReiDoChopp.Domain.RestockingProducts.Repositories.Filters
{
    public class RestockingProductsListFilter : PaginationFilter
    {
        public int? Id { get; set; }
        public int? RestockingId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? UnitPricePaid { get; set; }


        public RestockingProductsListFilter() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) {}
    }
}