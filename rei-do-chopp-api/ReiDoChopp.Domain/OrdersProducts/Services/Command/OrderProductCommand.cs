namespace ReiDoChopp.Domain.OrdersProducts.Services.Command
{
    public class OrderProductCommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double UnitPriceCharged { get; set; }
        public double? UnitPrice { get; set; }
    }
}
