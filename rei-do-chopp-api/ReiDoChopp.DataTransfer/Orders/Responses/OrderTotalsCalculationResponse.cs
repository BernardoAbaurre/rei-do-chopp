namespace ReiDoChopp.DataTransfer.Orders.Responses
{
    public class OrderTotalsCalculationResponse
    {
        public double TotalFees { get; set; }
        public double TotalProducts { get; set; }
        public double ExpectedTotalProducts { get; set; }
        public double ExpectedTotal { get; set; }
        public double Total { get; set; }
    }
}
