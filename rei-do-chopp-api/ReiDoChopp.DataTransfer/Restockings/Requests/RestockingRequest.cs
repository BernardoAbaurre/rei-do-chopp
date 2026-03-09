using ReiDoChopp.DataTransfer.RestockingAdditionalFees.Requests;
using ReiDoChopp.DataTransfer.RestockingProducts.Requests;

namespace ReiDoChopp.DataTransfer.Restockings.Request
{
    public class RestockingRequest
    {
        public DateTime Date { get; set; }
        public double? Discount { get; set; }
        public RestockingProductRequest[] RestockingProducts { get; set; }
        public RestockingAdditionalFeeRequest[] RestockingAdditionalFees { get; set; }
    }
}
