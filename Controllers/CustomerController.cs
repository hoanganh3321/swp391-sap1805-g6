using BackEnd.Attributes;
using BackEnd.Extensions;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerController(ICustomerService customerService, IHttpContextAccessor httpContextAccessor)
        {
            _customerService = customerService;
            _httpContextAccessor = httpContextAccessor;
        }

        //https://localhost:7002/api/customer/register        
        [HttpPost("register")]
        [StaffAuthorize]
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
        //https://localhost:7002/api/customer/getall
        [HttpGet("getall")]
        [StaffAuthorize]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            int? staffId = _httpContextAccessor.HttpContext.GetStaffId();
            if (staffId == null)
            {
                return BadRequest("Phiên đăng nhập hết hạn");
            }
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        //https://localhost:7002/api/customer/update/{id}
        [HttpPut("update/{id}")]
        [StaffAuthorize]
        public async Task<ActionResult> UpdateCustomer(int id, Customer customer)
        {
            int? staffId = _httpContextAccessor.HttpContext.GetStaffId();
            if (staffId == null)
            {
                return BadRequest("Phiên đăng nhập hết hạn");
            }
            if (id != customer.CustomerId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _customerService.UpdateCustomerAsync(customer);
            return NoContent();
        }

        //https://localhost:7002/api/customer/delete/{id}
        [HttpDelete("delete/{id}")]
        [StaffAuthorize]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            int? staffId = _httpContextAccessor.HttpContext.GetStaffId();
            if (staffId == null)
            {
                return BadRequest("Phiên đăng nhập hết hạn");
            }
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }

        //https://localhost:7002/api/customer/searchcustomer?lastName={}
        [HttpGet("searchcustomer")]
        [StaffAuthorize]
        public async Task<IActionResult> GetCustomer([FromQuery] string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                return BadRequest("LastName cannot be null or empty.");
            }

            int? staffId = _httpContextAccessor.HttpContext.GetStaffId();
            if (staffId == null)
            {
                return BadRequest("Phiên đăng nhập hết hạn");
            }

            var customer = await _customerService.GetCustomertByLastNameAsync(lastName);
            if (customer == null)
            {
                return NotFound("No customer found with the provided last name.");
            }

            return Ok(customer);
        }
    }
}
