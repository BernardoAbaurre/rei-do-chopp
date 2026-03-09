namespace ReiDoChopp.Domain.Products.Entities
{
    public class Product
    {
        public int Id { get; protected set; }
        public string BarCode { get; protected set; }
        public string Description { get; protected set; }
        public double SellingPrice { get; protected set; }
        public int StockQuantity { get; protected set; }
        public bool Active { get; protected set; }
        public int AlertQuantity { get; protected set; }

        protected Product() { }
        public Product(string barCode, string description, double sellingPrice, int stockQuantity, int alertQuantity)
        {
            SetBarCode(barCode);
            SetDescription(description);
            SetSellingPrice(sellingPrice);
            SetStockQuantity(stockQuantity);
            SetActive(true);
            SetAlertQuantity(alertQuantity);
        }

        public virtual void SetBarCode(string barCode)
        {
            BarCode = barCode;
        }
        public virtual void SetDescription(string description)
        {
            Description = description;
        }
        public virtual void SetSellingPrice(double sellingPrice)
        {
            SellingPrice = sellingPrice;
        }
        public virtual void SetStockQuantity(int stockQuantity)
        {
            StockQuantity = stockQuantity;
        }
        public virtual void SetActive(bool active)
        {
            Active = active;
        }
        public virtual void SetAlertQuantity(int alertQuantity)
        {
            AlertQuantity = alertQuantity;
        }
    }
}
