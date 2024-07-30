using System.Linq.Expressions;

namespace WA.Domain.Interfaces
{
    public interface IRepository : IDisposable
    {
    }

    public interface IRepository<T> : IDisposable where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T?>> GetAllAsync();
        Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<List<T?>> GetListByFilterAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy = null, string orderByDescending = "ASC", int? take = null, params Expression<Func<T, object>>[] includes);
        Task<T> CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entities);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> expression);

    }
}
