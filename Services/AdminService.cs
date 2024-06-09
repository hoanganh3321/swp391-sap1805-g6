using BackEnd.Models;
using BackEnd.Reporitories;
using BackEnd.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEnd.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;

        public AdminService(IAdminRepository adminRepository, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
        }

        public string GenerateJwtToken(Admin admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"] ?? "");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString()),
                new Claim(ClaimTypes.Email, admin.Email ?? ""),
                new Claim(ClaimTypes.Role, "Admin")
            }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> LoginAsync(LoginRequest loginRequest)
        {
            var admin = await _adminRepository.GetAdminByEmailAsync(loginRequest.Email);
            if (admin == null) { return "Invalid email."; }
            if ( admin.Password != loginRequest.Password)
            {
                return "Invalid password.";
            }
            var token = GenerateJwtToken(admin);
            return token;
        }

        public Task LogoutAsync()
        {
            // Invalidate token (if using a token store, e.g., database)
            return Task.CompletedTask;
        }
    }
}
