namespace ReiDoChopp.Domain.Utils.Models
{
    public class PaginationModel<T> where T : class
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public IList<T> Data { get; set; }
    }
}
