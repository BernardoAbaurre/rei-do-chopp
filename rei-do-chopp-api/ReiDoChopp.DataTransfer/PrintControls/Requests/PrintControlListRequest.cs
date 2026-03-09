using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Pagination.Requests;
using ReiDoChopp.Domain.PrintControls.Enums;

namespace ReiDoChopp.DataTransfer.PrintControls.Requests
{
    public class PrintControlListRequest : PaginationRequest
    {
        public int? Id { get; set; }
        public PrintControlStatusEnum? Status { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }


        public PrintControlListRequest() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) {}
    }
}