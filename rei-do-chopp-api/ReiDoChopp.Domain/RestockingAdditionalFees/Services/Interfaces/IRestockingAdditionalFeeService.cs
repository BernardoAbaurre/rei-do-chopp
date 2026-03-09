using ReiDoChopp.Domain.RestockingAdditionalFees.Entities;
using ReiDoChopp.Domain.RestockingAdditionalFees.Services.Command;
using ReiDoChopp.Domain.Restockings.Entities;

namespace ReiDoChopp.Domain.RestockingAdditionalFees.Services.Interfaces
{
    public interface IRestockingAdditionalFeesService
    {
        Task<RestockingAdditionalFee> ValidateAsync(int id);
        void Instantiate(RestockingAdditionalFeeCommand[] commands, Restocking restocking);
    }
}
