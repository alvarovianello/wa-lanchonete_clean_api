using WA.Domain.Entities;

namespace WA.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetStatusOrderAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> GetOrderByOrderNumber(string orderNumber);
        Task<IEnumerable<Order>> GetOrderByStatusAsync(string status);
        Task<Order> CreateOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> UpdateOrderStatusAsync(int id, string status);
        Task<bool> DeleteOrderAsync(int id);
    }
}
