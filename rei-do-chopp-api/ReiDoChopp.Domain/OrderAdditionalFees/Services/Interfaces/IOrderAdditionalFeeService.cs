using System.Threading.Tasks;
using ReiDoChopp.Domain.OrderAdditionalFees.Entities;
using ReiDoChopp.Domain.OrderAdditionalFees.Services.Commands;
using ReiDoChopp.Domain.Orders.Entities;

namespace ReiDoChopp.Domain.OrderAdditionalFees.Services.Interfaces
{
    public interface IOrderAdditionalFeesService
    {
        Task<OrderAdditionalFee> ValidateAsync(int id);
        void Instantiate(OrderAdditionalFeeCommand[] commands, Order order);
    }
}
