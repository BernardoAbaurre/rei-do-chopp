using ReiDoChopp.Domain.Restockings.Entities;
using ReiDoChopp.Domain.Restockings.Services.Commands;

namespace ReiDoChopp.Domain.Restockings.Services.Interfaces
{
    public interface IRestockingsService
    {
        Task<Restocking> ValidateAsync(int id);
        Task<Restocking> InsertAsync(RestockingCommand command);
        Task<Restocking> EditAsync(int id, RestockingCommand command);
    }
}
