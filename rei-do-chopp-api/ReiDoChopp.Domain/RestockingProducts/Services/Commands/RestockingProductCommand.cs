using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReiDoChopp.Domain.RestockingProducts.Services.Commands
{
    public class RestockingProductCommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double UnitPricePaid { get; set; }
    }
}
