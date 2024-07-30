using System.Linq.Expressions;
using WA.Domain.Entities;

namespace WA.Domain.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task AddPayment(Payment customer);
        Task<Payment> GetPaymentByStatus(string status);
        Task<Payment> GetPaymentByOrderNumberAsync(int orderId);
        Task<IEnumerable<Payment>> GetPayments();
    }
}
