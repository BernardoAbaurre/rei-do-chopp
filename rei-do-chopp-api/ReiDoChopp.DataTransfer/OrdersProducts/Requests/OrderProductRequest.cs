using System;

namespace ReiDoChopp.DataTransfer.OrdersProducts.Request
{
    public class OrderProductRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double UnitPriceCharged { get; set; }
        public double UnitPrice { get; set; }
    }
}
