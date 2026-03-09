using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Pagination.Requests;

namespace ReiDoChopp.DataTransfer.RestockingAdditionalFees.Requests
{
    public class RestockingAdditionalFeeListRequest : PaginationRequest
    {
        public int? Id { get; set; }
        public int? RestockingId { get; set; }
        public double? Value { get; set; }
        public string Description { get; set; }


        public RestockingAdditionalFeeListRequest() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) {}
    }
}