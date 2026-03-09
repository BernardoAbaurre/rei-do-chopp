using System;

namespace ReiDoChopp.DataTransfer.OpertaionsHistories.Requests
{
    public class OperationHistoryListRequest
    {
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public int[] ProductsIds { get; set; }
    }
}
