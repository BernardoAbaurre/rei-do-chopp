using ReiDoChopp.Domain.Utils.Enums;

namespace ReiDoChopp.Domain.Utils.Filters
{
    public class PaginationFilter
    {
        private int pageSize;
        public int Page { get; set; }
        public int PageSize {
            get => pageSize;
            set => pageSize = (value < 100 ? value : 100);
        }
        public string OrdenationField { get; set; }
        public OrdenationTypeEnum OrdenationType { get; set; }

        public PaginationFilter(int pageSize, string ordenationField, OrdenationTypeEnum ordenationType)
        {
            Page = 1;
            PageSize = pageSize;
            OrdenationField = ordenationField;
            OrdenationType = ordenationType;
        }
    }
}
