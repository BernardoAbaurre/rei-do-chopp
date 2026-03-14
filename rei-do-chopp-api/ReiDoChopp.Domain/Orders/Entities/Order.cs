using ReiDoChopp.Domain.OrderAdditionalFees.Entities;
using ReiDoChopp.Domain.OrdersProducts.Entities;
using ReiDoChopp.Domain.Users.Entities;

namespace ReiDoChopp.Domain.Orders.Entities
{
    public class Order
    {
        public int Id { get; protected set; }
        public virtual User Cashier { get; protected set; }
        public int CashierId { get; protected set; }
        public virtual User Attendant { get; protected set; }
        public int AttendantId { get; protected set; }
        public DateTime OrderDate { get; protected set; }
        public double? Discount { get; protected set; }

        public virtual IList<OrderAdditionalFee> OrderAdditionalFees { get; set; } = new List<OrderAdditionalFee>();
        public virtual IList<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        protected Order() { }

        public Order(User cashier, User attendant, DateTime orderDate, double? discount)
        {
            SetCashier(cashier);
            SetAttendant(attendant);
            SetOrderDate(orderDate);
            SetDiscount(discount);
        }

        public virtual void SetCashier(User cashier)
        {
            Cashier = cashier;
            CashierId = cashier.Id;
        }
        public virtual void SetAttendant(User attendant)
        {
            Attendant = attendant;
            AttendantId = attendant.Id;
        }

        public virtual void SetOrderDate(DateTime orderDate)
        {
            if (orderDate <= DateTime.MinValue)
                throw new ArgumentException("Required field: Order");

            OrderDate = DateTime.SpecifyKind(orderDate, DateTimeKind.Utc);
        }

        public virtual void SetDiscount(double? discount)
        {
            Discount = discount;
        }
    }
}
