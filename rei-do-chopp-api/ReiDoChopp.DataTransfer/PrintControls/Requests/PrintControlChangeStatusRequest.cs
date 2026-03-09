using ReiDoChopp.Domain.PrintControls.Enums;
using System;

namespace ReiDoChopp.DataTransfer.PrintControls.Requests
{
    public class PrintControlChangeStatusRequest
    {
        public PrintControlStatusEnum Status { get; set; }
    }
}
