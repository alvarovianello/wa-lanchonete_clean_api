using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WA.Application.Contracts.Request.RequestCategory;
using WA.Application.Interfaces;
using WA.Application.Validators.ValidatorsCategory;
using WA.Domain.Base;
using WA.Domain.Entities;
using WA.Domain.Interfaces;

namespace WA.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async ValueTask<IActionResult> RegisterCategory(CategoryPostRequest categoryRequest)
        {
            var validator = await new CategoryPostRequestValidator().ValidateAsync(categoryRequest);

            if (!validator.IsValid)
                return new ResultObject(HttpStatusCode.BadRequest, validator);

            try
            {
                var returnGetCategoryByName = await _categoryRepository.GetCategoryByName(categoryRequest.Name);

                if (returnGetCategoryByName != null)
                    return new ResultObject(HttpStatusCode.AlreadyReported, new { Warn = "O nome de categoria informado já possui cadastro" });

                Category category = _mapper.Map<Category>(categoryRequest);
                await _categoryRepository.RegisterCategory(category);

                if (category == null)
                    return new ResultObject(HttpStatusCode.BadRequest, new { Error = "Houve um erro ao realizar o cadastro da categoria" });

                return new ResultObject(HttpStatusCode.OK, new { Success = "Categoria cadastrada com sucesso" });

            }
            catch (Exception ex)
            {
                return new ResultObject(HttpStatusCode.InternalServerError, new { Error = ex.Message });
            }
        }

        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                Category category = await _categoryRepository.GetCategoryById(id);

                if (category == null)
                    return new ResultObject(HttpStatusCode.NotFound, new { Info = "Categoria não encontrada" });
                else
                    return new ResultObject(HttpStatusCode.OK, category);
            }
            catch (Exception ex)
            {
                return new ResultObject(HttpStatusCode.InternalServerError, new { Error = ex.Message });
            }
        }

        public async Task<IActionResult> GetCategoryByName(string name)
        {
            try
            {
                Category category = await _categoryRepository.GetCategoryByName(name);

                if (category == null)
                    return new ResultObject(HttpStatusCode.NotFound, new { Info = "Categoria não encontrada" });
                else
                    return new ResultObject(HttpStatusCode.OK, category);
            }
            catch (Exception ex)
            {
                return new ResultObject(HttpStatusCode.InternalServerError, new { Error = ex.Message });
            }
        }

        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var listCategories = await _categoryRepository.GetAllCategories();
                return new ResultObject(HttpStatusCode.OK, listCategories);
            }
            catch (Exception ex)
            {
                return new ResultObject(HttpStatusCode.BadRequest, ex);
            }
        }

        public async Task<IActionResult> UpdateCategory(CategoryPutRequest categoryPutRequest)
        {
            var validator = await new CategoryPutRequestValidator().ValidateAsync(categoryPutRequest);

            if (!validator.IsValid)
                return new ResultObject(HttpStatusCode.BadRequest, validator);

            try
            {
                var returnGetCategoryByName = await _categoryRepository.GetCategoryByName(categoryPutRequest.Name);

                if (returnGetCategoryByName != null)
                    return new ResultObject(HttpStatusCode.AlreadyReported, new { Warn = "O nome de categoria informado já possui cadastro" });

                returnGetCategoryByName = _mapper.Map<Category>(categoryPutRequest);
                await _categoryRepository.UpdateCategory(returnGetCategoryByName);

                if (returnGetCategoryByName == null)
                    return new ResultObject(HttpStatusCode.BadRequest, new { Error = "Houve um erro ao realizar a alteração da categoria" });

                return new ResultObject(HttpStatusCode.OK, returnGetCategoryByName);

            }
            catch (Exception ex)
            {
                return new ResultObject(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
