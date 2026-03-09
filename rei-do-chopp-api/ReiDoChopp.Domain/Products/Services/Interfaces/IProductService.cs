using System.Threading.Tasks;
using ReiDoChopp.Domain.Products.Entities;

namespace ReiDoChopp.Domain.Products.Services.Interfaces
{
    public interface IProductsService
    {
        Task<Product> ValidateAsync(int id);
        Task<Product> InsertAsync(string barCode, string description, double sellingPrice, int stockQuantity, int alertQuantity);
        Task<Product> EditAsync(int id, string barCode, string description, double sellingPrice, int alertQuantity);
        void AddStockQuantity(Product product, int quantity);
        Product ChangeStatus(Product product);
    }
}
