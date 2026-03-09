using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReiDoChopp.Domain.OrderAdditionalFees.Services.Commands
{
    public class OrderAdditionalFeeCommand
    {
        public double Value { get; set; }
        public string Description { get; set; }
    }
}
