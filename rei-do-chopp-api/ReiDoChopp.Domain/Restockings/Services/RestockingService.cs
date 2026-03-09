using ReiDoChopp.Domain.Products.Repositories;
using ReiDoChopp.Domain.RestockingAdditionalFees.Services.Interfaces;
using ReiDoChopp.Domain.RestockingProducts.Services.Interfaces;
using ReiDoChopp.Domain.Restockings.Entities;
using ReiDoChopp.Domain.Restockings.Repositories;
using ReiDoChopp.Domain.Restockings.Services.Commands;
using ReiDoChopp.Domain.Restockings.Services.Interfaces;
using ReiDoChopp.Domain.Utils.Exceptions;

namespace ReiDoChopp.Domain.Restockings.Services
{
    public class RestockingsService : IRestockingsService
    {
        private readonly IRestockingsRepository restockingsRepository;
        private readonly IProductsRepository productsRepository;
        private readonly IRestockingProductsService restockingProductsService;
        private readonly IRestockingAdditionalFeesService restockingAdditionalFeesService;

        public RestockingsService(
            IRestockingsRepository restockingsRepository,
            IProductsRepository productsRepository,
            IRestockingProductsService restockingProductsService,
            IRestockingAdditionalFeesService restockingAdditionalFeesService
        )
        {
            this.restockingsRepository = restockingsRepository;
            this.productsRepository = productsRepository;
            this.restockingProductsService = restockingProductsService;
            this.restockingAdditionalFeesService = restockingAdditionalFeesService;
        }

        public async Task<Restocking> EditAsync(int id, RestockingCommand command)
        {
            Restocking restocking = await ValidateAsync(id);

            restocking.SetDate(command.Date);
            restocking.SetUser(command.User);
            restocking.SetDiscount(command.Discount);

            restockingProductsService.Instantiate(command.RestockingProducts, restocking, true);
            restockingAdditionalFeesService.Instantiate(command.RestockingAdditionalFees, restocking);

            restockingsRepository.Edit(restocking);

            return restocking;
        }

        public async Task<Restocking> InsertAsync(RestockingCommand command)
        {
            Restocking restocking = new Restocking(command.Date, command.User, command.Discount);

            restockingProductsService.Instantiate(command.RestockingProducts, restocking);
            restockingAdditionalFeesService.Instantiate(command.RestockingAdditionalFees, restocking);

            await restockingsRepository.InsertAsync(restocking);

            return restocking;
        }

        public async Task<Restocking> ValidateAsync(int id)
        {
            Restocking entity = await restockingsRepository.FindByIdAsync(id);

            if (entity == null)
            {
                throw new RegisterNotFound(id);
            }

            return entity;
        }
    }
}
