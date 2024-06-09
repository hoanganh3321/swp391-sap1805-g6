using BackEnd.Attributes;
using BackEnd.Filters;
using BackEnd.Models;
using BackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        //https://localhost:7002/api/product/add
        //ok
        [HttpPost("add")]
        [ProductValidationFilter]
        [AdminAuthorize]
        
        public async Task<IActionResult> AddProduct( Product product)
        {
            var newProduct = await _productService.AddProductAsync(product);
            return Ok(newProduct);
        }

        //https://localhost:7002/api/product/search/{id}
        //ok
        [HttpGet("search/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        //https://localhost:7002/api/product/list
        //ok
        [HttpGet("list")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        //https://localhost:7002/api/product/update/{id}
        //ok
        [HttpPut("update/{id}")]
        [AdminAuthorize]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            var updated = await _productService.UpdateProductAsync(id, product);
            if (!updated)
            {
                return NotFound();
            }
            return Ok();
        }
        //https://localhost:7002/api/product/delete/{id}
        //ok
        [HttpDelete("delete/{id}")]
        [AdminAuthorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProductAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
