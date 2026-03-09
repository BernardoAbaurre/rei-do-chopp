using ReiDoChopp.Domain.Products.Entities;
using ReiDoChopp.Domain.Restockings.Entities;

namespace ReiDoChopp.Domain.RestockingProducts.Entities
{
    public class RestockingProduct
    {
        public int Id { get; protected set; }
        public virtual Restocking Restocking { get; protected set; }
        public int RestockingId { get; protected set; }
        public virtual Product Product { get; protected set; }
        public int ProductId { get; protected set; }
        public int Quantity { get; protected set; }
        public double UnitPricePaid { get; protected set; }
        public double TotalPricePaid { get; protected set; }

        protected RestockingProduct() { }

        public RestockingProduct(Restocking restocking, Product product, int quantity, double unitPricePaid)
        {
            SetRestocking(restocking);
            SetProduct(product);
            SetQuantity(quantity);
            SetUnitPricePaid(unitPricePaid);
            SetCalculationValues();
        }

        public virtual void SetRestocking(Restocking restocking)
        {
            Restocking = restocking;
            RestockingId = restocking.Id;
        }
        public virtual void SetProduct(Product product)
        {
            Product = product;
            ProductId = product.Id;
        }
        public virtual void SetQuantity(int quantity)
        {
            if (quantity == null)
                throw new ArgumentException("Required field: Quantity");
            Quantity = quantity;
        }
        public virtual void SetUnitPricePaid(double unitPricePaid)
        {
            if (unitPricePaid == null)
                throw new ArgumentException("Required field: UnitPricePaid");
            UnitPricePaid = unitPricePaid;
        }

        public virtual void SetTotalPricePaid(double totalPricePaid)
        {
            if (totalPricePaid == null)
                throw new ArgumentException("Required field: TotalPricePaid");
            TotalPricePaid = totalPricePaid;
        }

        public virtual void SetCalculationValues()
        {
             SetTotalPricePaid(UnitPricePaid * Quantity);
        }
    }
}
