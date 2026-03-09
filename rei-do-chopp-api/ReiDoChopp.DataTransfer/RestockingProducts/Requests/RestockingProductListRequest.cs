using ReiDoChopp.DataTransfer.Pagination.Requests;
using ReiDoChopp.Domain.Utils.Enums;

namespace ReiDoChopp.DataTransfer.RestockingProducts.Requests
{
    public class RestockingProductListRequest : PaginationRequest
    {
        public int? Id { get; set; }
        public int? RestockingId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? UnitPricePaid { get; set; }


        public RestockingProductListRequest() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) { }
    }
}