using BackEnd.Attributes;
using BackEnd.Extensions;
using BackEnd.Models;
using BackEnd.Services;
using BackEnd.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffOrderController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly ICustomerService _customerService;
        private readonly ILoyalPointService _loyalPointService;


        public StaffOrderController(IOrderDetailService orderDetailService,
            ICustomerService customerService,
            ILoyalPointService loyalPointService)
                                                    
        {
            _orderDetailService = orderDetailService;

            _customerService = customerService;
            _loyalPointService = loyalPointService;

        }

        //https://localhost:7002/api/StaffOrder/register
        [HttpPost("register")]
        [StaffAuthorize]
        //ok
        public async Task<IActionResult> RegisterCustomer([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registeredCustomer = await _customerService.RegisterCustomerAsync(customer);
            return Ok(registeredCustomer);
        }

        //https://localhost:7002/api/StaffOrder/searchcustomer?lastName={}
        [HttpGet("searchcustomer")]
        [StaffAuthorize]
        //ok
        public async Task<IActionResult> GetCustomer([FromQuery] string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                return BadRequest("LastName cannot be null or empty.");
            }

            var customer = await _customerService.GetCustomertByLastNameAsync(lastName);
            if (customer == null)
            {
                return NotFound("No customer found with the provided last name.");
            }

            return Ok(customer);
        }


        //https://localhost:7002/api/StaffOrder/addProductToCart
        //ok
        [HttpPost("addProductToCart")]
        [StaffAuthorize]
        public async Task<IActionResult> AddToCart([FromBody] OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _orderDetailService.StaffAddToCartAsync(model);                       
                //loyalPoint
                await _loyalPointService.AddAsync(model);
                return Ok("Product added to cart successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //https://localhost:7002/api/StaffOrder/DeleteFromCart
        //ok
        [HttpPut("DeleteFromCart")]
        [StaffAuthorize]
        public async Task<IActionResult> DeleteFromCart([FromBody] StaffDeleteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            { 
                //loyalPoint
                await _loyalPointService.DeleteAsync(model);
                await _orderDetailService.StaffDeleteFromCartAsync(model);
                
                return Ok("Product was Deleted from cart");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //https://localhost:7002/api/StaffOrder/viewCart/{id}
        //ok
        [HttpGet("viewCart/{customerId}")]
        [StaffAuthorize]
        public async Task<IActionResult> viewCart(int customerId)
        {
            try
            {
                var orderDetails = await _orderDetailService.StaffViewCartAsync(customerId);
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
        
    }
}
