using BackEnd.Services;
using BackEnd.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        //https://localhost:7002/api/admin/login
        //ok
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var token = await _adminService.LoginAsync(loginRequest);
            if (token == null)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok(new { Token = token });
        }
        //https://localhost:7002/api/admin/logout
        //ok
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _adminService.LogoutAsync();
            return Ok("Logged out successfully");
        }
    }
}
