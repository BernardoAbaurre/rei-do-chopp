using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Pagination.Requests;

namespace ReiDoChopp.DataTransfer.Restockings.Request
{
    public class RestockingListRequest : PaginationRequest
    {
        public int? Id { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public int? UserId { get; set; }
        public double? ProductSumValue { get; set; }
        public double? TotalFees { get; set; }
        public int[] ProductsIds { get; set; }

        public RestockingListRequest() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.DESC) {}
    }
}