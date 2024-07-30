using Microsoft.AspNetCore.Mvc;
using WA.Application.Contracts.Request.RequestCategory;
using WA.Application.Interfaces;

namespace WA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCategory(CategoryPostRequest category)
        {
            var returnRegisterCategory = await _categoryService.RegisterCategory(category);

            if (returnRegisterCategory == null)
                return NotFound();

            return returnRegisterCategory;
        }

        [HttpGet("getCategoryById/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category == null)
                return NotFound();

            return category;
        }

        [HttpGet("getCategoryByName/{name}")]
        public async Task<IActionResult> GetCategoryByName(string name)
        {
            var category = await _categoryService.GetCategoryByName(name);

            if (category == null)
                return NotFound();

            return category;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return await _categoryService.GetAllCategories();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryPutRequest categoryPutRequest)
        {
            var returnUpdateCategory = await _categoryService.UpdateCategory(categoryPutRequest);

            if (returnUpdateCategory == null)
                return NotFound();

            return returnUpdateCategory;
        }
    }
}
