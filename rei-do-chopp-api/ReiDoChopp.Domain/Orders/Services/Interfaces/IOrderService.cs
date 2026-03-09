using System.Threading.Tasks;
using ReiDoChopp.Domain.Orders.Entities;
using ReiDoChopp.Domain.Orders.Services.Commands;
using ReiDoChopp.Domain.PrintControls.Entities;
using ReiDoChopp.Domain.Users.Entities;

namespace ReiDoChopp.Domain.Orders.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<Order> ValidateAsync(int id);
        Task<Order> InsertAsync(OrderCommand command);
        Task<Order> EditAsync(int id, OrderCommand command);
        Task<PrintControl> PrintAsync(Order order, User user);
    }
}
