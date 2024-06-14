﻿using BackEnd.Services;
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

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
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



    }
}