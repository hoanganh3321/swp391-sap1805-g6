using BackEnd.Attributes;
using BackEnd.Extensions;
using BackEnd.Services;
using BackEnd.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(IOrderDetailService orderDetailService,
            IHttpContextAccessor httpContextAccessor)
        {
            _orderDetailService = orderDetailService;          
            _httpContextAccessor = httpContextAccessor;
        }
        //https://localhost:7002/api/cart/addProductToCart
        //ok
        [HttpPost("addProductToCart")]
        [JwtAuthorization]
        public async Task<IActionResult> AddToCart([FromBody]AddToCartModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderDetailService.AddToCartAsync(model);
                return Ok("Product added to cart successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //https://localhost:7002/api/cart/viewCart
        //ok
        [HttpGet("viewCart")]
        [JwtAuthorization]
        public async Task<IActionResult> ViewCart()
        {
            try
            {
                int? customerId = _httpContextAccessor.HttpContext.GetCustomerId();
                var orderDetails = await _orderDetailService.ViewCartAsync(customerId);
                if (!orderDetails.Any())
                {
                    return NotFound("No items found in the cart");
                }
                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //https://localhost:7002/api/cart/DeleteFromCart
        //ok
        [HttpPut("DeleteFromCart")]
        [JwtAuthorization]
        public async Task<IActionResult> DeleteFromCart([FromBody] DeleteCartViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _orderDetailService.DeleteFromCartAsync(model);
                return Ok("Product was Deleted from cart");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

