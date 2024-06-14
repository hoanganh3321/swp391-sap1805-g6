
using BackEnd.Models;

namespace BackEnd.Reporitories
{
    public interface IStaffRepository
    {
        Task<Staff?> GetAdminByEmailAsync(string email);
        Task<Store> GetStoreByStaffIdAsync(int? staffId);
    }
}
