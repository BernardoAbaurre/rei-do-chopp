namespace ReiDoChopp.Domain.Products.Repositories.Models
{
    public class ProductModel
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
