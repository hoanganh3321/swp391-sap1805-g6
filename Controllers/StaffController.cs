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
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StaffController(IStaffService staffService, IHttpContextAccessor httpContextAccessor)
        {
            _staffService = staffService;
            _httpContextAccessor = httpContextAccessor;
        }

        //https://localhost:7002/api/staff/login
        //ok
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string jwttoken = await _staffService.LoginAsync(loginRequest);
            return Ok(new { jwttoken });

        }
        //https://localhost:7002/api/staff/logout
        //ok
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _staffService.LogoutAsync();
            return Ok("Logged out successfully");
        }

        //https://localhost:7002/api/staff/search/?email={}
        [HttpGet("search")]
        [AdminAuthorize]
        public async Task<IActionResult> GetStaff( [FromQuery] string email)
        {
            int? admin = _httpContextAccessor.HttpContext.GetAdminId();
            if (admin == null) { throw new Exception("phien dang nhap het han"); }
            var staff = await _staffService.GetStaffByEmailAsync(email);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        //https://localhost:7002/api/staff/list
        [HttpGet("list")]
        [AdminAuthorize]
        public async Task<IActionResult> GetAllStaff()
        {
            int? admin = _httpContextAccessor.HttpContext.GetAdminId();
            if (admin == null) { throw new Exception("phien dang nhap het han"); }
            var staff = await _staffService.GetAllStaff();
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        //https://localhost:7002/api/staff/add
        [HttpPost("add")]
        [AdminAuthorize]

        public async Task<IActionResult> AddStaff(Staff staff)
        {
            int? admin = _httpContextAccessor.HttpContext.GetAdminId();
            if (admin == null) { throw new Exception("phien dang nhap het han");}
            var newStaff = await _staffService.AddStaffAsync(staff);
            return Ok(newStaff);
        }
    }
}
