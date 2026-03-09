using System;


namespace ReiDoChopp.DataTransfer.RestockingAdditionalFees.Response
{
    public class RestockingAdditionalFeeResponse
    {
        public int Id { get; set; }
        public int RestockingId { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
    }
}
