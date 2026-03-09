using ReiDoChopp.Domain.Products.Entities;
using ReiDoChopp.Domain.Products.Repositories;
using ReiDoChopp.Domain.Products.Services.Interfaces;
using ReiDoChopp.Domain.Utils.Exceptions;

namespace ReiDoChopp.Domain.Products.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<Product> EditAsync(int id, string barCode, string description, double sellingPrice, int alertQuantity)
        {
            Product entity = await ValidateAsync(id);

            entity.SetBarCode(barCode);
            entity.SetDescription(description);
            entity.SetSellingPrice(sellingPrice);
            entity.SetAlertQuantity(alertQuantity);

            productsRepository.Edit(entity);

            return entity;
        }

        public async Task<Product> InsertAsync(string barCode, string description, double sellingPrice, int stockQuantity, int alertQuantity)
        {
            Product produtoConflitante = productsRepository.Query().FirstOrDefault(p => p.BarCode == barCode);

            if (produtoConflitante != null)
                throw new Exception($"Já existe um produto com esse código de barras cadastrado: {produtoConflitante.Description}");

            Product entity = new Product(barCode, description, sellingPrice, stockQuantity, alertQuantity);

            await productsRepository.InsertAsync(entity);

            return entity;
        }

        public async Task<Product> ValidateAsync(int id)
        {
            Product entity = await productsRepository.FindByIdAsync(id);

            if (entity == null)
            {
                throw new RegisterNotFound(id);
            }

            return entity;
        }

        public void AddStockQuantity(Product product, int quantity)
        {
            int finalStockQuantity = product.StockQuantity + quantity;

            if(finalStockQuantity < 0)
            {
                throw new Exception($"Esta quantidade não faz sentido");
            }

            product.SetStockQuantity(finalStockQuantity);

            productsRepository.Edit(product);
        }

        public Product ChangeStatus(Product product)
        {
            product.SetActive(!product.Active);
            productsRepository.Edit(product);

            return product;
        }
    }
}
