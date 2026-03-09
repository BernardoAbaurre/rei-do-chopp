namespace ReiDoChopp.DataTransfer.Pagination.Responses
{
    public class PaginationResponse<T> where T : class
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public IList<T> Data { get; set; }
    }
}
