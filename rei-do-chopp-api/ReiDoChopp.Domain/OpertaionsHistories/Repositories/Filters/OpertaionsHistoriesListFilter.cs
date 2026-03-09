namespace ReiDoChopp.Domain.OpertaionsHistories.Repositories.Filters
{
    public class OpertaionsHistoriesListFilter
    {
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public int[] ProductsIds { get; set; }


        public OpertaionsHistoriesListFilter() { }
    }
}