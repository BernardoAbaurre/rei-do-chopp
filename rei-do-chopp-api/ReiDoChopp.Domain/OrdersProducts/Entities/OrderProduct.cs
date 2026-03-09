using ReiDoChopp.Domain.Orders.Entities;
using ReiDoChopp.Domain.Products.Entities;

namespace ReiDoChopp.Domain.OrdersProducts.Entities
{
    public class OrderProduct
    {
        public int Id { get; protected set; }
        public virtual Order Order { get; protected set; }
        public int OrderId { get; protected set; }
        public virtual Product Product { get; protected set; }
        public int ProductId { get; protected set; }
        public int Quantity { get; protected set; }
        public double UnitPriceCharged { get; protected set; }
        public double ExpectedUnitPrice { get; protected set; }
        public double TotalPriceCharged { get; protected set; }
        public double ExpectedTotalPrice { get; protected set; }

        protected OrderProduct() { }

        public OrderProduct(Order order, Product product, int quantity, double unitPriceCharged)
        {
            SetOrder(order);
            SetProduct(product);
            SetQuantity(quantity);
            SetUnitPriceCharged(unitPriceCharged);
            SetCalculationValues();
        }

        public virtual void SetOrder(Order order)
        {
            Order = order;
            OrderId = order.Id;
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
        public virtual void SetUnitPriceCharged(double unitPriceCharged)
        {
            if (unitPriceCharged == null)
                throw new ArgumentException("Required field: UnitPriceCharged");
            UnitPriceCharged = unitPriceCharged;
        }

        public virtual void SetExpectedUnitPrice(double expectedUnitPrice)
        {
            ExpectedUnitPrice = expectedUnitPrice;
        }

        public virtual void SetTotalPriceCharged(double totalPriceCharged)
        {
            TotalPriceCharged = totalPriceCharged;
        }

        public virtual void SetExpectedTotalPrice(double expectedTotalPrice)
        {
            ExpectedTotalPrice = expectedTotalPrice;
        }

         public virtual void SetCalculationValues()
         {
             SetExpectedUnitPrice(Product.SellingPrice);
             SetExpectedTotalPrice(ExpectedUnitPrice * Quantity);
             SetTotalPriceCharged(UnitPriceCharged * Quantity);
         }
    }
}
