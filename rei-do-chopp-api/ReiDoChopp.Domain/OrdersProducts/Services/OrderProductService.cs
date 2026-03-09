using ReiDoChopp.Domain.Orders.Entities;
using ReiDoChopp.Domain.OrdersProducts.Entities;
using ReiDoChopp.Domain.OrdersProducts.Repositories;
using ReiDoChopp.Domain.OrdersProducts.Services.Command;
using ReiDoChopp.Domain.OrdersProducts.Services.Interfaces;
using ReiDoChopp.Domain.Products.Entities;
using ReiDoChopp.Domain.Products.Repositories;
using ReiDoChopp.Domain.Products.Services.Interfaces;
using ReiDoChopp.Domain.Restockings.Entities;
using ReiDoChopp.Domain.Utils.Exceptions;

namespace ReiDoChopp.Domain.OrdersProducts.Services
{
    public class OrdersProductsService : IOrdersProductsService
    {
        private readonly IOrdersProductsRepository ordersProductsRepository;
        private readonly IProductsRepository productsRepository;
        private readonly IProductsService productsService;

        public OrdersProductsService(
            IOrdersProductsRepository ordersProductsRepository,
            IProductsRepository productsRepository,
            IProductsService productsService
        )
        {
            this.ordersProductsRepository = ordersProductsRepository;
            this.productsRepository = productsRepository;
            this.productsService = productsService;
        }

        public async Task<OrderProduct> ValidateAsync(int id)
        {
            OrderProduct entity = await ordersProductsRepository.FindByIdAsync(id);
            if (entity == null)
            {
                throw new RegisterNotFound(id);
            }
            return entity;
        }

        public void Instantiate(OrderProductCommand[] commands, Order order, bool editing = false)
        {
            List<Product> products = productsRepository.Query().Where(p => commands.Select(cp => cp.ProductId).Contains(p.Id)).ToList();
            products.AddRange(order.OrderProducts.Select(op => op.Product).Where(p => !commands.Select(c => c.ProductId).Contains(p.Id)));

            foreach (Product product in products)
            {

                int commandQuantity = commands.Where(p => p.ProductId == product.Id).Sum(p => p.Quantity);
                int currentOrderProductsQuantity = 0;

                if (product.StockQuantity - commandQuantity < 0)
                    throw new Exception($"Não há itens suficientes do produto {product.Description} em estoque. Tem apenas {product.StockQuantity} unidades");

                 if (!product.Active)
                    product.SetActive(true);

                if (editing)
                {
                    currentOrderProductsQuantity = order.OrderProducts.Where(op => op.Product == product).Sum(op => op.Quantity);
                }

                productsService.AddStockQuantity(product, currentOrderProductsQuantity - commandQuantity);
            }

            order.OrderProducts.Clear();

            foreach (OrderProductCommand productCommand in commands)
            {
                Product product = products.FirstOrDefault(p => p.Id == productCommand.ProductId);
                OrderProduct orderProduct = new(order, product, productCommand.Quantity, productCommand.UnitPriceCharged);

                order.OrderProducts.Add(orderProduct);
            }
        }

        public void Delete(Order order)
        {
            var products = order.OrderProducts.GroupBy(p => p.Product)
                .Select(g => new { Product = g.Key, Quantity = g.Sum(rp => rp.Quantity) });

            foreach (var product in products)
            {
                productsService.AddStockQuantity(product.Product, product.Quantity);
            }

            order.OrderProducts.Clear();
        }
    }
}
