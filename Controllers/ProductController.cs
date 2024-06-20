using BackEnd.Attributes;
using BackEnd.Extensions;
using BackEnd.Filters;
using BackEnd.Models;
using BackEnd.Services;
using BackEnd.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductReturnService _productReturnService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductController(IProductService productService, IHttpContextAccessor httpContextAccessor, IProductReturnService productReturnService)
        {
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
            _productReturnService = productReturnService;
        }

        //https://localhost:7002/api/product/AddReturnProduct
        //
        [HttpPost("AddReturnProduct")]
        [StaffAuthorize]

        public async Task<IActionResult> AddReturnProduct(ProductReturnViewModel product)
        {
            int? staff = _httpContextAccessor.HttpContext.GetStaffId();
            if (staff == null)
            {
                return Unauthorized("Session has expired");
            }
            var result = await _productReturnService.CreateProductReturnAsync(product);
            if (result.IsNewProduct)
            {
                return Ok(new { Message = "New product added", Product = result.Product });
            }
            else
            {
                return Ok(new { Message = "Product return processed", ProductReturn = result.ProductReturn });
            }
        }


        //https://localhost:7002/api/product/add
        //ok
        [HttpPost("add")]
        [ProductValidationFilter]
        [AdminAuthorize]

        public async Task<IActionResult> AddProduct( Product product)
        {
            int? admin = _httpContextAccessor.HttpContext.GetAdminId();
            if (admin == null) { throw new Exception("phien dang nhap het han"); }
            var newProduct = await _productService.AddProductAsync(product);
            return Ok(newProduct);
        }
        //htt
        //https://localhost:7002/api/product/search/{id}
        //ok
        //get lay tu trong du lieu database
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
        //put update trong database
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            int? admin = _httpContextAccessor.HttpContext.GetAdminId();
            if (admin == null) { throw new Exception("phien dang nhap het han"); }
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
            int? admin = _httpContextAccessor.HttpContext.GetAdminId();
            if (admin == null) { throw new Exception("phien dang nhap het han");}
            var deleted = await _productService.DeleteProductAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
