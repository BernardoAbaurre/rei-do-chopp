using ReiDoChopp.Domain.OrderAdditionalFees.Services.Interfaces;
using ReiDoChopp.Domain.Orders.Entities;
using ReiDoChopp.Domain.Orders.Repositories;
using ReiDoChopp.Domain.Orders.Services.Commands;
using ReiDoChopp.Domain.Orders.Services.Interfaces;
using ReiDoChopp.Domain.OrdersProducts.Services.Interfaces;
using ReiDoChopp.Domain.PrintControls.Entities;
using ReiDoChopp.Domain.PrintControls.Services.Interfaces;
using ReiDoChopp.Domain.Users.Entities;
using ReiDoChopp.Domain.Utils.Exceptions;
using ReiDoChopp.Domain.Utils.Extensions;
using ReiDoChopp.Infra.Helpers.Repositories;
using System.Text;

namespace ReiDoChopp.Domain.Orders.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly IOrdersProductsService ordersProductsService;
        private readonly IOrderAdditionalFeesService orderAdditionalFeesService;
        private readonly IPrintControlsService printControlsService;

        public OrdersService(
            IOrdersRepository ordersRepository,
            IOrdersProductsService ordersProductsService,
            IOrderAdditionalFeesService orderAdditionalFeesService,
            IPrintControlsService printControlsService
        )
        {
            this.ordersRepository = ordersRepository;
            this.ordersProductsService = ordersProductsService;
            this.orderAdditionalFeesService = orderAdditionalFeesService;
            this.printControlsService = printControlsService;
        }

        public async Task<Order> EditAsync(int id, OrderCommand command)
        {
            Order order = await ValidateAsync(id);

            order.SetCashier(command.Cashier);
            order.SetAttendant(command.Attendant);
            order.SetOrderDate(command.OrderDate);
            order.SetDiscount(command.Discount);

            order.OrderAdditionalFees.Clear();

            ordersProductsService.Instantiate(command.OrderProducts, order, true);
            orderAdditionalFeesService.Instantiate(command.OrderAdditionalFees, order);

            ordersRepository.Edit(order);

            return order;
        }

        public async Task<Order> InsertAsync(OrderCommand command)
        {
            Order order = new Order(command.Cashier, command.Attendant, command.OrderDate, command.Discount);

            ordersProductsService.Instantiate(command.OrderProducts, order, false);
            orderAdditionalFeesService.Instantiate(command.OrderAdditionalFees, order);
            
            await ordersRepository.InsertAsync(order);

            return order;
        }

        public async Task<Order> ValidateAsync(int id)
        {
            Order entity = await ordersRepository.FindByIdAsync(id);

            if (entity == null)
            {
                throw new RegisterNotFound(id);
            }

            return entity;
        }

        public async Task<PrintControl> PrintAsync(Order order, User user)
        {
            StringBuilder text = new();

            foreach (var orderProduct in order.OrderProducts)
            {
                var product = orderProduct.Product.Description.FormatTextSize(32);
                var quantity = $"{orderProduct.Quantity}UN".FormatTextSize(4);
                var unitPriceCharged = orderProduct.UnitPriceCharged.ToBrazilianCurrency();
                var totalPriceCharged = (orderProduct.UnitPriceCharged * orderProduct.Quantity).ToBrazilianCurrency();

                text.AppendLine($"[BOLD]{product}[TEXT_NORMAL]");
                text.AppendLine($" {quantity} {unitPriceCharged.FormatTextSize(7)} {totalPriceCharged.FormatTextSize(7)}");
            }

            foreach (var additionalFee in order.OrderAdditionalFees)
            {
                var description = additionalFee.Description.FormatTextSize(32);
                var value = additionalFee.Value.ToBrazilianCurrency();

                text.AppendLine($"[BOLD]{description}[TEXT_NORMAL]");
                text.AppendLine($" {value.FormatTextSize(7)}");
            }


            text.Append("[LINE]");

            double totalProducts = order.OrderProducts?.Sum(op => op.UnitPriceCharged * op.Quantity) ?? 0;
            double expectedTotalProducts = order.OrderProducts?.Sum(op => op.Product.SellingPrice * op.Quantity) ?? 0;
            double totalFees = order.OrderAdditionalFees?.Sum(af => af.Value) ?? 0;
            double fullValue = (totalProducts * (1 - ((order.Discount ?? 0)/100))) + totalFees;

            text.AppendLine("Subtotal:" + totalProducts.ToBrazilianCurrency().PadLeft(23));
            text.AppendLine("Adicionais:" + totalFees.ToBrazilianCurrency().PadLeft(21));
            text.AppendLine("Desconto:" + (expectedTotalProducts - totalProducts).ToBrazilianCurrency().PadLeft(23));
            text.AppendLine("A Pagar:" + fullValue.ToBrazilianCurrency().PadLeft(24));
            text.Append("[LINE]");

            return await printControlsService.InsertAsync(user, text.ToString());

        }
    }
}
