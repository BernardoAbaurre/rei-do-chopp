namespace ReiDoChopp.Domain.OpertaionsHistories.Repositories.Models
{
    public class OperationHistoryModel
    {
        public double ExpectedTotalProductsRevenue { get; set; }
        public double TotalProductsRevenue { get; set; }
        public double TotalChargedAdditionalFees { get; set; }
        public double TotalDiscount { get; set; }
        public double TotalExpense { get; set; }
        public double TotalRevenue { get; set; }
        public double TotalProfit { get; set; }
        public double TotalProfitMargin { get; set; }
    }
}
