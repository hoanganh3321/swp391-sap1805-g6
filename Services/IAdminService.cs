using BackEnd.ViewModels;

namespace BackEnd.Services
{
    public interface IAdminService
    {
        Task<string> LoginAsync(LoginRequest loginRequest);
        Task LogoutAsync();
    }
}
