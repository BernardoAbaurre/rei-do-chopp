using ReiDoChopp.DataTransfer.Users.Responses;

namespace ReiDoChopp.DataTransfer.Orders.Responses
{
    public class OrderHistoryResponse
    {
        public int Id { get; set; }
        public string Cashier { get; set; }
        public string Attendant { get; set; }
        public DateTime OrderDate { get; set; }
        public double ExpectedProductsSumValue { get; set; }
        public double RealProductsSumValue { get; set; }
        public double? Discount { get; set; }
        public double? TotalFees { get; set; }
        public double TotalValue { get; set; }
    }
}
