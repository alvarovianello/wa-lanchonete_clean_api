using WA.Application.Interfaces;
using WA.Domain.Entities;
using WA.Domain.Interfaces;

namespace WA.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Orderstatus> _orderStatusRepository;
        private readonly IRepository<Orderitem> _orderItensRepository;
        private readonly IRepository<Payment> _orderPaymentRepository;

        public OrderService(IOrderRepository orderRepository, IRepository<Orderstatus> orderStatusRepository, IRepository<Orderitem> orderItensRepository, IRepository<Payment> orderPaymentRepository, IRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
            _orderItensRepository = orderItensRepository;
            _orderPaymentRepository = orderPaymentRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<IEnumerable<Order>> GetStatusOrderAsync()
        {
            return await _orderRepository.GetStatusOrderAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetOrderByFilterAsync(o => o.Id == id);
        }

        public async Task<Order> GetOrderByOrderNumber(string orderNumber)
        {
            return await _orderRepository.GetOrderByFilterAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task<IEnumerable<Order>> GetOrderByStatusAsync(string status)
        {
            return await _orderRepository.GetOrderListByFilterAsync(o => o.Status == status);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {

            foreach (var orderItem in order.Orderitems)
            {
                var product = await _productRepository.GetByIdAsync(orderItem.ProductId.Value);
                orderItem.Price = product?.Price;
            }

            order.OrderNumber = await GenerateOrderCodeAsync();
            order.Status = "Recebido";
            order.TotalPrice = order.Orderitems.Sum(x => x.Price * x.Quantity.Value);

            var createdOrder = await _orderRepository.CreateAsync(order);

            var orderStatus = new Orderstatus
            {
                OrderId = createdOrder.Id,
                Status = "Recebido"
            };

            await _orderStatusRepository.CreateAsync(orderStatus);

            return createdOrder;
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, string status)
        {
            var order = await _orderRepository.GetOrderByFilterAsync(o => o.Id == id);
            order.Status = status;

            var orderStatus = new Orderstatus
            {
                OrderId = order.Id,
                Status = status
            };

            await _orderStatusRepository.CreateAsync(orderStatus);

            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetOrderByFilterAsync(o => o.Id == id);

            if (order == null)
            {
                return false;
            }

            // Exclua os status associados
            foreach (var orderItem in order.Orderstatuses.ToList())
            {
                await _orderStatusRepository.DeleteAsync(orderItem.Id);
            }
            // Exclua os itens associados
            foreach (var orderItem in order.Orderitems.ToList())
            {
                await _orderItensRepository.DeleteAsync(orderItem.Id);
            }
            // Exclua os pagamentos associados
            foreach (var orderItem in order.Payments)
            {
                await _orderPaymentRepository.DeleteAsync(orderItem.Id);
            }

            // Agora exclua o pedido
            return await _orderRepository.DeleteAsync(id);

        }

        private async Task<string> GenerateOrderCodeAsync()
        {
            string orderCode;
            Random random = new Random();
            bool isUnique;

            do
            {
                orderCode = random.Next(10000, 99999).ToString();
                isUnique = !await _orderRepository.GetUnicOrderNumberAsync(orderCode);
            } while (!isUnique);

            return orderCode;
        }
    }
}
