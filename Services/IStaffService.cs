using BackEnd.Models;
using BackEnd.ViewModels;

namespace BackEnd.Services
{
    public interface IStaffService
    {
        Task<string> LoginAsync(LoginRequest loginRequest);
        Task LogoutAsync();
        Task<Staff?> GetStaffByEmailAsync(string email);
        Task<IEnumerable<Staff>> GetAllStaff();
        Task<Staff?> AddStaffAsync(Staff staff);

    }
}
