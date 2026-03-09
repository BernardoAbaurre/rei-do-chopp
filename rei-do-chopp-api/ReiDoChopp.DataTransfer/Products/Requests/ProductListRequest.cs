using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Pagination.Requests;

namespace ReiDoChopp.DataTransfer.Products.Request
{
    public class ProductListRequest : PaginationRequest
    {
        public int? Id { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public double? SellingPrice { get; set; }
        public int? StockQuantity { get; set; }
        public string DescriptionOrBarCode { get; set; }
        public bool? Active { get; set; }
        public bool? Alert { get; set; }

        public ProductListRequest() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) {}

    }
}