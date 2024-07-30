using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WA.Application.Contracts.Request.RequestProduct;
using WA.Application.Interfaces;
using WA.Domain.Entities;

namespace WA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductRequest>>(products);
            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<ProductRequest>(product);
            return Ok(productDto);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            var productDtos = _mapper.Map<IEnumerable<ProductRequest>>(products);
            return Ok(productDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var createdProduct = await _productService.CreateProductAsync(product);
            var createdProductDto = _mapper.Map<ProductRequest>(createdProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProductDto.Id }, createdProductDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequest productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(productDto);
            var result = await _productService.UpdateProductAsync(product);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}