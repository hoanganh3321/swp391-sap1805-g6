using BackEnd.ViewModels;

namespace BackEnd.Services
{
    public interface IStaffService
    {
        Task<string> LoginAsync(LoginRequest loginRequest);
        Task LogoutAsync();
    }
}
