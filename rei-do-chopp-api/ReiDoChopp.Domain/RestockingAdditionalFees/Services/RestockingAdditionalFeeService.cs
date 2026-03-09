using ReiDoChopp.Domain.RestockingAdditionalFees.Entities;
using ReiDoChopp.Domain.RestockingAdditionalFees.Repositories;
using ReiDoChopp.Domain.RestockingAdditionalFees.Services.Command;
using ReiDoChopp.Domain.RestockingAdditionalFees.Services.Interfaces;
using ReiDoChopp.Domain.Restockings.Entities;
using ReiDoChopp.Domain.Utils.Exceptions;

namespace ReiDoChopp.Domain.RestockingAdditionalFees.Services
{
    public class RestockingAdditionalFeesService : IRestockingAdditionalFeesService
    {
        private readonly IRestockingAdditionalFeesRepository restockingAdditionalFeesRepository;

        public RestockingAdditionalFeesService(IRestockingAdditionalFeesRepository restockingAdditionalFeesRepository)
        {
            this.restockingAdditionalFeesRepository = restockingAdditionalFeesRepository;
        }

        public async Task<RestockingAdditionalFee> ValidateAsync(int id)
        {
            RestockingAdditionalFee entity = await restockingAdditionalFeesRepository.FindByIdAsync(id);

            if (entity == null)
            {
                throw new RegisterNotFound(id);
            }

            return entity;
        }

        public void Instantiate(RestockingAdditionalFeeCommand[] commands, Restocking restocking)
        {
            restocking.RestockingAdditionalFees.Clear();

            foreach (RestockingAdditionalFeeCommand command in commands)
            {
                RestockingAdditionalFee restockingAdditionalFee = new(restocking, command.Value, command.Description);
                restocking.RestockingAdditionalFees.Add(restockingAdditionalFee);
            }
        }
    }
}
