using ReiDoChopp.Domain.Utils.Enums;

namespace ReiDoChopp.DataTransfer.Pagination.Requests
{
    public class PaginationRequest
    {
        private int pageSize;
        public int Page { get; set; }
        public int PageSize {
            get => pageSize;
            set => pageSize = (value < 100 ? value : 100);
        }
        public string OrdenationField { get; set; }
        public OrdenationTypeEnum OrdenationType { get; set; }

        public PaginationRequest(int pageSize, string ordenationField, OrdenationTypeEnum ordenationType)
        {
            Page = 1;
            PageSize = pageSize;
            OrdenationField = ordenationField;
            OrdenationType = ordenationType;
        }
    }
}
