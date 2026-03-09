namespace ReiDoChopp.DataTransfer.Restockings.Responses
{
    public class RestockingHistoryResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public double ProductsSumValue { get; set; }
        public double Discount { get; set; }
        public double TotalFees { get; set; }
        public double TotalValue { get; set; }
    }
}
