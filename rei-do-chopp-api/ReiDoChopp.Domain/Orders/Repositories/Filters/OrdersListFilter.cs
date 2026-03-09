using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.Domain.Utils.Filters;

namespace ReiDoChopp.Domain.Orders.Repositories.Filters
{
    public class OrdersListFilter : PaginationFilter
    {
        public int? Id { get; set; }
        public int? CashierId { get; set; }
        public int? AttendantId { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public double? Discount { get; set; }
        public int[] ProductsIds { get; set; }

        public OrdersListFilter() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) {}
    }
}