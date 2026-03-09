using ReiDoChopp.Domain.RestockingAdditionalFees.Entities;
using ReiDoChopp.Domain.RestockingProducts.Entities;
using ReiDoChopp.Domain.Users.Entities;

namespace ReiDoChopp.Domain.Restockings.Entities
{
    public class Restocking
    {
        public int Id { get; protected set; }
        public DateTime Date { get; protected set; }
        public double? Discount { get; protected set; }
        public virtual User User { get; protected set; }
        public int UserId { get; protected set; }

        public virtual IList<RestockingProduct> RestockingProducts { get; set; } = new List<RestockingProduct>();
        public virtual IList<RestockingAdditionalFee> RestockingAdditionalFees { get; set; } = new List<RestockingAdditionalFee>();

        protected Restocking() { }

        public Restocking(DateTime date, User user, double? discount)
        {
            SetDate(date);
            SetUser(user);
            SetDiscount(discount);
        }

        public virtual void SetDate(DateTime date)
        {
            if (date == DateTime.MinValue)
                throw new ArgumentException("Required field: Date");
            Date = date;
        }
        public virtual void SetUser(User user)
        {
            User = user;
            UserId = user.Id;
        }

        public virtual void SetDiscount(double? discount)
        {
            Discount = discount;
        }
    }
}
