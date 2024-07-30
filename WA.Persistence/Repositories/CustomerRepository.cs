using System.Linq.Expressions;
using WA.Domain.Entities;
using WA.Domain.Interfaces;
using WA.Persistence.Context;

namespace WA.Persistence.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context) { }

        public async Task AddCustomer(Customer customer)
        {
            await CreateAsync(customer);
        }
        public async Task<Customer> GetCustomerByCPF(string cpf)
        {
            Expression<Func<Customer, bool>> predicate = entity => entity.Cpf == cpf;
            return await GetSingleAsync(predicate);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomer()
        {
            return await GetAllAsync();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            Expression<Func<Customer, bool>> predicate = entity => entity.Id == id;
            return await GetSingleAsync(predicate);
        }

        public async Task UpdateCustomer(Customer customer)
        {
            await UpdateAsync(customer);
        }

        public async Task RemoveCustomer(int id)
        {
            await DeleteAsync(id);
        }
    }
}