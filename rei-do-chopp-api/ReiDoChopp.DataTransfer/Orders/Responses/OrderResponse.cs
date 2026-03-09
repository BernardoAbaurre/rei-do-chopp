using ReiDoChopp.DataTransfer.OrderAdditionalFees.Response;
using ReiDoChopp.DataTransfer.OrdersProducts.Response;
using ReiDoChopp.DataTransfer.Users.Responses;
using System;


namespace ReiDoChopp.DataTransfer.Orders.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string CashierName { get; set; }
        public string AttendantName { get; set; }
        public int CashierId { get; set; }
        public int AttendantId { get; set; }
        public DateTime OrderDate { get; set; }
        public double? Discount { get; set; }
        public IList<OrderProductResponse> OrderProducts { get; set; }
        public IList<OrderAdditionalFeeResponse> OrderAdditionalFees { get; set; }
    }
}
