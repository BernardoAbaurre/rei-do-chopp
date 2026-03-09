using ReiDoChopp.Domain.OrderAdditionalFees.Services.Commands;
using ReiDoChopp.Domain.OrdersProducts.Services.Command;
using ReiDoChopp.Domain.Users.Entities;

namespace ReiDoChopp.Domain.Orders.Services.Commands
{
    public class OrderCommand
    {
        public User Cashier { get; set; }
        public User Attendant { get; set; }
        public DateTime OrderDate { get; set; }
        public double? Discount { get; set; }
        public OrderAdditionalFeeCommand[] OrderAdditionalFees { get; set; }
        public OrderProductCommand[] OrderProducts { get; set; }
    }
}
