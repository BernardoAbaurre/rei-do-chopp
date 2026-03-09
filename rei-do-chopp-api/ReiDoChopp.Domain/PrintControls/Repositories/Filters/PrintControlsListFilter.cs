using System;
using ReiDoChopp.Domain.PrintControls.Enums;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.Domain.Utils.Filters;

namespace ReiDoChopp.Domain.PrintControls.Repositories.Filters
{
    public class PrintControlsListFilter : PaginationFilter
    {
        public int? Id { get; set; }
        public PrintControlStatusEnum? Status { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }


        public PrintControlsListFilter() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) {}
    }
}