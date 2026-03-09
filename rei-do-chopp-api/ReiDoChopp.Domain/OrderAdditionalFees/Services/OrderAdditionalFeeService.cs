using System;
using System.Threading.Tasks;
using ReiDoChopp.Domain.OrderAdditionalFees.Entities;
using ReiDoChopp.Domain.OrderAdditionalFees.Repositories;
using ReiDoChopp.Domain.OrderAdditionalFees.Services.Commands;
using ReiDoChopp.Domain.OrderAdditionalFees.Services.Interfaces;
using ReiDoChopp.Domain.Orders.Entities;
using ReiDoChopp.Domain.Utils.Exceptions;

namespace ReiDoChopp.Domain.OrderAdditionalFees.Services
{
    public class OrderAdditionalFeesService : IOrderAdditionalFeesService
    {
        private readonly IOrderAdditionalFeesRepository orderAdditionalFeesRepository;

        public OrderAdditionalFeesService(IOrderAdditionalFeesRepository orderAdditionalFeesRepository)
        {
            this.orderAdditionalFeesRepository = orderAdditionalFeesRepository;
        }

        public void Instantiate(OrderAdditionalFeeCommand[] commands, Order order)
        {
            order.OrderAdditionalFees.Clear();

            foreach (var command in commands)
            {
                OrderAdditionalFee orderAdditionalFee = new(order, command.Value, command.Description);
                order.OrderAdditionalFees.Add(orderAdditionalFee);
            }
        }

        public async Task<OrderAdditionalFee> ValidateAsync(int id)
        {
            var entity = await orderAdditionalFeesRepository.FindByIdAsync(id);

            if (entity == null)
            {
                throw new RegisterNotFound(id);
            }

            return entity;
        }
    }
}
