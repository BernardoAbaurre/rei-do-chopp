using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Pagination.Requests;

namespace ReiDoChopp.DataTransfer.Orders.Request
{
    public class OrderListRequest : PaginationRequest
    {
        public int? Id { get; set; }
        public int? CashierId { get; set; }
        public int? AttendantId { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public double? Discount { get; set; }
        public int[] ProductsIds { get; set; }

        public OrderListRequest() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.DESC) {}
    }
}