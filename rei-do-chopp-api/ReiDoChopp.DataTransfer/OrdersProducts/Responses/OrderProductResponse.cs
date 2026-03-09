using ReiDoChopp.DataTransfer.Products.Response;

namespace ReiDoChopp.DataTransfer.OrdersProducts.Response
{
    public class OrderProductResponse
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public ProductResponse Product { get; set; }
        public int Quantity { get; set; }
        public double UnitPriceCharged { get; set; }
    }
}
