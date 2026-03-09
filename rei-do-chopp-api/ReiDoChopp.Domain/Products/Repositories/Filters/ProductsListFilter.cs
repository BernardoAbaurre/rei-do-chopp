using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.Domain.Utils.Filters;

namespace ReiDoChopp.Domain.Products.Repositories.Filters
{
public class ProductsListFilter : PaginationFilter
    {
        public int? Id { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public double? SellingPrice { get; set; }
        public int? StockQuantity { get; set; }
        public string DescriptionOrBarCode { get; set; }
        public bool? Active { get; set; }
        public bool? Alert { get; set; }

        public ProductsListFilter() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) {}
    }
}