using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WA.Domain.Entities;
using WA.Domain.Interfaces;
using WA.Persistence.Context;

namespace WA.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        protected readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByFilterAsync(Expression<Func<Order, bool>> predicate)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Orderstatuses)
                .Include(o => o.Payments)
                .Include(o => o.Orderitems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Order>> GetOrderListByFilterAsync(Expression<Func<Order, bool>> predicate)
        {
            return await _context.Orders.Where(predicate)
                .Include(o => o.Customer)
                .Include(o => o.Orderstatuses)
                .Include(o => o.Payments)
                .Include(o => o.Orderitems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Payments)
                .Include(o => o.Orderitems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetStatusOrderAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Payments)
                .Include(o => o.Orderitems)
                    .ThenInclude(oi => oi.Product)
                .Where(o => (o.Status == "Pronto" || o.Status == "Em Preparação" || o.Status == "Em preparação" || o.Status == "Recebido") &&
                o.Payments.Any(p => p.PaymentStatus == 2)) // Filtra pedidos com pagamento aprovado
                .Select(o => new
                {
                    Order = o,
                    StatusOrder = o.Status == "Pronto" ? 1 :
                      o.Status == "Em Preparação" || o.Status == "Em preparação" ? 2 :
                      o.Status == "Recebido" ? 3 : 4
                })
                .OrderBy(o => o.StatusOrder)
                .ThenBy(o => o.Order.CreatedAt)
                .Select(o => o.Order)
                .ToListAsync();
        }
        public async Task<bool> GetUnicOrderNumberAsync(string orderCode)
        {
            return await _context.Orders.AnyAsync(o => o.OrderNumber == orderCode);
        }




    }
}
