namespace ReiDoChopp.DataTransfer.RestockingProducts.Requests
{
    public class RestockingProductRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double UnitPricePaid { get; set; }
    }
}
