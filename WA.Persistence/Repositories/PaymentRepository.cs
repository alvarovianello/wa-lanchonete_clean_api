using System;
using System.Linq.Expressions;
using WA.Domain.Entities;
using WA.Domain.Interfaces;
using WA.Persistence.Context;

namespace WA.Persistence.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context) { }

        public Task AddPayment(Payment customer)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Payment>> GetPayments()
        {
            return await GetAllAsync();
        }

        public async Task<Payment> GetPaymentByOrderNumberAsync(int orderId)
        {
            Expression<Func<Payment, bool>> predicate = entity => entity.OrderId == orderId;
            return await GetSingleAsync(predicate);
        }

        public Task<Payment> GetPaymentByStatus(string status)
        {
            throw new NotImplementedException();
        }
    }
}
