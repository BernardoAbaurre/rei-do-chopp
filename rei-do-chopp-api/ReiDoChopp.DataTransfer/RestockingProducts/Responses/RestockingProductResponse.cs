using ReiDoChopp.DataTransfer.Products.Response;
using System;


namespace ReiDoChopp.DataTransfer.RestockingProducts.Response
{
    public class RestockingProductResponse
    {
        public int Id { get; set; }
        public int RestockingId { get; set; }
        public int ProductId { get; set; }
        public ProductResponse Product { get; set; }
        public int Quantity { get; set; }
        public double UnitPricePaid { get; set; }
    }
}
