using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WA.Domain.Interfaces;
using WA.Persistence.Context;

namespace WA.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private bool disposedValue;

        protected readonly DbSet<T> _set;
        protected readonly DbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<List<T?>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _set;

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                return await query.FirstAsync(predicate);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<List<T?>> GetListByFilterAsync(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>> orderBy = null,
        string orderByDescending = "ASC",
        int? take = null,
        params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _set.Where(predicate);

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                if (orderBy != null)
                {
                    query = orderByDescending == "DESC" ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
                }

                if (take.HasValue)
                {
                    query = query.Take(take.Value);
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await _set.CountAsync(expression);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _set.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {
            await _set.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _set.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _set.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}