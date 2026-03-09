using ReiDoChopp.Domain.Products.Entities;
using ReiDoChopp.Domain.Products.Repositories;
using ReiDoChopp.Domain.Products.Services.Interfaces;
using ReiDoChopp.Domain.RestockingProducts.Entities;
using ReiDoChopp.Domain.RestockingProducts.Repositories;
using ReiDoChopp.Domain.RestockingProducts.Services.Commands;
using ReiDoChopp.Domain.RestockingProducts.Services.Interfaces;
using ReiDoChopp.Domain.Restockings.Entities;
using ReiDoChopp.Domain.Utils.Exceptions;

namespace ReiDoChopp.Domain.RestockingProducts.Services
{
    public class RestockingProductsService : IRestockingProductsService
    {
        private readonly IRestockingProductsRepository restockingProductsRepository;
        private readonly IProductsRepository productsRepository;
        private readonly IProductsService productsService;

        public RestockingProductsService(
            IRestockingProductsRepository restockingProductsRepository,
            IProductsRepository productsRepository,
            IProductsService productsService
        )
        {
            this.restockingProductsRepository = restockingProductsRepository;
            this.productsRepository = productsRepository;
            this.productsService = productsService;
        }

        public void Instantiate(RestockingProductCommand[] commands, Restocking restocking, bool editing = false)
        {
            List<Product> products = productsRepository.Query().Where(p => commands.Select(cp => cp.ProductId).Contains(p.Id)).ToList();

            products.AddRange(restocking.RestockingProducts.Select(rp => rp.Product).Where(p => !commands.Select(cp => cp.ProductId).Contains(p.Id)));

            foreach (Product product in products)
            {
                int commandQuantity = commands.Where(p => p.ProductId == product.Id).Sum(p => p.Quantity);
                int currentRestockingProductsQuantity = 0;


                if (!product.Active)
                    product.SetActive(true);

                if (editing)
                {

                    currentRestockingProductsQuantity = restocking.RestockingProducts.Where(op => op.Product == product).Sum(op => op.Quantity);
                }

                productsService.AddStockQuantity(product, commandQuantity - currentRestockingProductsQuantity);
            }

            restocking.RestockingProducts.Clear();

            foreach (RestockingProductCommand productCommand in commands)
            {
                Product product = products.FirstOrDefault(p => p.Id == productCommand.ProductId);
                RestockingProduct restockingProduct = new(restocking, product, productCommand.Quantity, productCommand.UnitPricePaid);

                restocking.RestockingProducts.Add(restockingProduct);
            }
        }

        public void Delete(Restocking restocking)
        {
            var products = restocking.RestockingProducts.GroupBy(p => p.Product)
                .Select(g => new { Product = g.Key, Quantity = g.Sum(rp => rp.Quantity) });

            foreach (var product in products)
            {
                productsService.AddStockQuantity(product.Product, -product.Quantity);
            }

            restocking.RestockingProducts.Clear();
        }

        public async Task<RestockingProduct> ValidateAsync(int id)
        {
            RestockingProduct entity = await restockingProductsRepository.FindByIdAsync(id);

            if (entity == null)
            {
                throw new RegisterNotFound(id);
            }

            return entity;
        }
    }
}
