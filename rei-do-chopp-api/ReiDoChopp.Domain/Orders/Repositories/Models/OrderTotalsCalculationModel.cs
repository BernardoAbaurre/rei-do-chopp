namespace ReiDoChopp.Domain.Orders.Repositories.Models
{
    public class OrderTotalsCalculationModel
    {
        public double TotalFees { get; set; }
        public double TotalProducts { get; set; }
        public double ExpectedTotalProducts { get; set; }
        public double ExpectedTotal { get; set; }
        public double Total { get; set; }
    }
}
