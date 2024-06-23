using BackEnd.Models;
using BackEnd.Services;
using BackEnd.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        //https://localhost:7002/api/customer/register        
        [HttpPost("register")]
        //ok
        public async Task<IActionResult> RegisterCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registeredCustomer = await _customerService.RegisterCustomerAsync(customer);
            return Ok(registeredCustomer);
        }
        //https://localhost:7002/api/customer/login
        //ok
        [HttpPost("login")]
        
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            string jwttoken = await _customerService.LoginAsync(loginRequest);
            return Ok(new { jwttoken });

        }
    }
}
