using ReiDoChopp.DataTransfer.OrderAdditionalFees.Request;
using ReiDoChopp.DataTransfer.OrdersProducts.Request;
using System;

namespace ReiDoChopp.DataTransfer.Orders.Request
{
    public class OrderRequest
    {
        public int? AttendantId { get; set; }
        public DateTime OrderDate { get; set; }
        public double? Discount { get; set; }
        public OrderAdditionalFeeRequest[] OrderAdditionalFees { get; set; }
        public OrderProductRequest[] OrderProducts { get; set; }
    }
}
