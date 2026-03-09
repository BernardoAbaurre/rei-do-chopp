using System;
using ReiDoChopp.Domain.Utils.Enums;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Pagination.Requests;

namespace ReiDoChopp.DataTransfer.Users.Requests
{
    public class UserListRequest : PaginationRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public int[] RoleIds { get; set; }

        public UserListRequest() : base(pageSize: 10, ordenationField: "Id", ordenationType: OrdenationTypeEnum.ASC) {}
    }
}