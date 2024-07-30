using WA.Domain.Entities;

namespace WA.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task RegisterCategory(Category category);
        Task<Category> GetCategoryById(int id);
        Task<Category> GetCategoryByName(string name);
        Task<IEnumerable<Category>> GetAllCategories();
        Task UpdateCategory(Category category);

    }
}
