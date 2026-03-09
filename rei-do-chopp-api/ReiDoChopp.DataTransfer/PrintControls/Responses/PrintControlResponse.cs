using ReiDoChopp.DataTransfer.Users.Responses;
using ReiDoChopp.Domain.PrintControls.Enums;
using System;


namespace ReiDoChopp.DataTransfer.PrintControls.Response
{
    public class PrintControlResponse
    {
        public int Id { get; set; }
        public PrintControlStatusEnum Status { get; set; }
        public DateTime RequestDate { get; set; }
        public UserResponse User { get; set; }
        public string Content { get; set; }
    }
}
