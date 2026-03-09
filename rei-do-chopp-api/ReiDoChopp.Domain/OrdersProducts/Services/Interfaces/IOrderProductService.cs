using ReiDoChopp.Domain.Orders.Entities;
using ReiDoChopp.Domain.OrdersProducts.Entities;
using ReiDoChopp.Domain.OrdersProducts.Services.Command;
using ReiDoChopp.Domain.Restockings.Entities;

namespace ReiDoChopp.Domain.OrdersProducts.Services.Interfaces
{
    public interface IOrdersProductsService
    {
        Task<OrderProduct> ValidateAsync(int id);
        void Instantiate(OrderProductCommand[] commands, Order order, bool editing = false);
        void Delete(Order order);
    }
}
