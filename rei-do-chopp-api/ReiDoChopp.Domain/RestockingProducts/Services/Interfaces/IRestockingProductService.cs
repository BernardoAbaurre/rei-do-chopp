using System.Threading.Tasks;
using ReiDoChopp.Domain.RestockingProducts.Entities;
using ReiDoChopp.Domain.RestockingProducts.Services.Commands;
using ReiDoChopp.Domain.Restockings.Entities;

namespace ReiDoChopp.Domain.RestockingProducts.Services.Interfaces
{
    public interface IRestockingProductsService
    {
        Task<RestockingProduct> ValidateAsync(int id);
        void Instantiate(RestockingProductCommand[] commands, Restocking restocking, bool editing = false);
        void Delete(Restocking restocking);
    }
}
