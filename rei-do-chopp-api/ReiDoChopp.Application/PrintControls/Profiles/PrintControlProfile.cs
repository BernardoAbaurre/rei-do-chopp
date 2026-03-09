using AutoMapper;
using ReiDoChopp.DataTransfer.PrintControls.Requests;
using ReiDoChopp.DataTransfer.PrintControls.Response;
using ReiDoChopp.Domain.PrintControls.Entities;
using ReiDoChopp.Domain.PrintControls.Repositories.Filters;

namespace ReiDoChopp.Application.PrintControls.Profiles
{
    public class PrintControlsProfile : Profile
    {
        public PrintControlsProfile()
        {
            CreateMap<PrintControl, PrintControlResponse>();

            CreateMap<PrintControlListRequest, PrintControlsListFilter>();
        }
    }
}
