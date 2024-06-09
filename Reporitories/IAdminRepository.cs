using BackEnd.Models;

namespace BackEnd.Reporitories
{
    public interface IAdminRepository
    {
        Task<Admin?> GetAdminByEmailAsync(string email);
    }
}
