using System.Linq.Expressions;
using WA.Domain.Entities;
using WA.Domain.Interfaces;
using WA.Persistence.Context;

namespace WA.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task RegisterCategory(Category category)
        {
            await CreateAsync(category);
        }

        public async Task<Category> GetCategoryById(int id)
        {
            Expression<Func<Category, bool>> predicate = entity => entity.Id == id;
            return await GetSingleAsync(predicate);
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            Expression<Func<Category, bool>> predicate = entity => entity.Name == name;
            return await GetSingleAsync(predicate);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await GetAllAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            await UpdateAsync(category);
        }
    }
}
