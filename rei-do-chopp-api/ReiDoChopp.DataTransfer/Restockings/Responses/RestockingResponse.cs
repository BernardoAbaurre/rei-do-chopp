using ReiDoChopp.DataTransfer.RestockingAdditionalFees.Response;
using ReiDoChopp.DataTransfer.RestockingProducts.Response;


namespace ReiDoChopp.DataTransfer.Restockings.Response
{
    public class RestockingResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public IList<RestockingAdditionalFeeResponse> RestockingAdditionalFees { get; set; }
        public IList<RestockingProductResponse> RestockingProducts { get; set; }
    }
}
