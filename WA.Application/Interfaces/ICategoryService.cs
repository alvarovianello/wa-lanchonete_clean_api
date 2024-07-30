using Microsoft.AspNetCore.Mvc;
using WA.Application.Contracts.Request.RequestCategory;

namespace WA.Application.Interfaces
{
    public interface ICategoryService
    {
        ValueTask<IActionResult> RegisterCategory(CategoryPostRequest categoryRequest);
        Task<IActionResult> GetCategoryById(int id);
        Task<IActionResult> GetCategoryByName(string name);
        Task<IActionResult> GetAllCategories();
        Task<IActionResult> UpdateCategory(CategoryPutRequest categoryPutRequest);
    }
}
