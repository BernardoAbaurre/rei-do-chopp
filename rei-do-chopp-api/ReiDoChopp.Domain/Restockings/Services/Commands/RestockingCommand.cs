using ReiDoChopp.Domain.RestockingAdditionalFees.Services.Command;
using ReiDoChopp.Domain.RestockingProducts.Services.Commands;
using ReiDoChopp.Domain.Users.Entities;

namespace ReiDoChopp.Domain.Restockings.Services.Commands
{
    public class RestockingCommand
    {
        public DateTime Date { get; set; }
        public User User { get; set; }
        public double? Discount { get; set; }
        public RestockingProductCommand[] RestockingProducts { get; set; }
        public RestockingAdditionalFeeCommand[] RestockingAdditionalFees { get; set; }
    }
}
