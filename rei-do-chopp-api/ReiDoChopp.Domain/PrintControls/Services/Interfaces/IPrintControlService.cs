using System.Threading.Tasks;
using ReiDoChopp.Domain.PrintControls.Entities;
using ReiDoChopp.Domain.PrintControls.Enums;
using ReiDoChopp.Domain.Users.Entities;

namespace ReiDoChopp.Domain.PrintControls.Services.Interfaces
{
    public interface IPrintControlsService
    {
        Task<PrintControl> ValidateAsync(int id);
        Task<PrintControl> InsertAsync(User user, string content);
        Task<PrintControl> ChangeStatusAsync(int id, PrintControlStatusEnum status);
    }
}
