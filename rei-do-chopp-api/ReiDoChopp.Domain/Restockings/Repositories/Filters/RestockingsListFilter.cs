using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.Domain.Utils.Filters;

namespace ReiDoChopp.Domain.Restockings.Repositories.Filters
{
    public class RestockingsListFilter : PaginationFilter
    {
        public int? Id { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public int? UserId { get; set; }
        public double? ProductSumValue { get; set; }
        public double? TotalFees { get; set; }
        public int[] ProductsIds { get; set; }

        public RestockingsListFilter() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) {}
    }
}