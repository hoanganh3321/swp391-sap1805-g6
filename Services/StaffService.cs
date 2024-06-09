using BackEnd.Models;
using BackEnd.Reporitories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackEnd.ViewModels;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IConfiguration _configuration;

        public StaffService(IStaffRepository staffRepository, IConfiguration configuration)
        {
            _staffRepository = staffRepository;
            _configuration = configuration;
        }

        public string GenerateJwtToken(Staff staff)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"] ?? "");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, staff.StaffId.ToString()),
                new Claim(ClaimTypes.Email, staff.Email ?? ""),
                new Claim(ClaimTypes.Role, "Staff")
            }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> LoginAsync(LoginRequest loginRequest)
        {
            var staff = await _staffRepository.GetAdminByEmailAsync(loginRequest.Email);
            if (staff == null) { return "Invalid email."; }
            if (staff.Password != loginRequest.Password)
            {
                return "Invalid password.";
            }
            var token = GenerateJwtToken(staff);
            return token;
        }

        public Task LogoutAsync()
        {
            // Invalidate token (if using a token store, e.g., database)
            return Task.CompletedTask;
        }
    }
}
