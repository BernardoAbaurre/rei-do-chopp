using ReiDoChopp.Domain.Orders.Entities;

namespace ReiDoChopp.Domain.OrderAdditionalFees.Entities
{
    public class OrderAdditionalFee
    {
        public int Id { get; protected set; }
        public virtual Order Order { get; protected set; }
        public int OrderId { get; protected set; }
        public double Value { get; protected set; }
        public string Description { get; protected set; }

        protected OrderAdditionalFee() { }

        public OrderAdditionalFee(Order order, double value, string description)
        {
            SetOrder(order);
            SetValue(value);
            SetDescription(description);
        }

        public virtual void SetOrder(Order order)
        {
            Order = order;
            OrderId = order.Id;
        }
        public virtual void SetValue(double value)
        {
            if (value == null)
                throw new ArgumentException("Required field: Value");

            Value = value;
        }
        public virtual void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Required field: Description");

            Description = description;
        }

    }
}
