using System.Linq.Expressions;
using WA.Domain.Entities;

namespace WA.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOrderByFilterAsync(Expression<Func<Order, bool>> predicate);
        Task<IEnumerable<Order>> GetOrderListByFilterAsync(Expression<Func<Order, bool>> predicate);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetStatusOrderAsync();
        Task<bool> GetUnicOrderNumberAsync(string orderCode);
    }
}
