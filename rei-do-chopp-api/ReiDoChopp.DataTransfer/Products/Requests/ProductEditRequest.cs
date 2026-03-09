using System;

namespace ReiDoChopp.DataTransfer.Products.Request
{
    public class ProductEditRequest
    {
        public string BarCode { get; set; }
        public string Description { get; set; }
        public double SellingPrice { get; set; }
        public int AlertQuantity { get; set; }
    }
}
