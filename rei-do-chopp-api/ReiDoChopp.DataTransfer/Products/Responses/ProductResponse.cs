using System;

namespace ReiDoChopp.DataTransfer.Products.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public double SellingPrice { get; set; }
        public int StockQuantity { get; set; }
        public bool Active { get; set; }
        public int AlertQuantity { get; set; }
        public bool Alert { get; set; }
    }
}
