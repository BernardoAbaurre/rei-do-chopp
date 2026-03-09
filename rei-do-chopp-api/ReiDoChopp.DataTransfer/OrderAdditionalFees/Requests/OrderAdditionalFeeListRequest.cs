using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Pagination.Requests;

namespace ReiDoChopp.DataTransfer.OrderAdditionalFees.Request
{
    public class OrderAdditionalFeeListRequest : PaginationRequest
    {
        public int? Id { get; set; }
        public int? OrderId { get; set; }
        public double? Value { get; set; }
        public string Description { get; set; }


        public OrderAdditionalFeeListRequest() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) {}
    }
}