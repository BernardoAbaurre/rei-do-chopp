using System;


namespace ReiDoChopp.DataTransfer.OrderAdditionalFees.Response
{
    public class OrderAdditionalFeeResponse
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
    }
}
