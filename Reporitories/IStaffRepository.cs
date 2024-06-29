
using BackEnd.Models;

namespace BackEnd.Reporitories
{
    public interface IStaffRepository
    {
        Task<Staff?> GetAdminByEmailAsync(string email);
        Task<Store> GetStoreByStaffIdAsync(int? staffId);
        Task<Staff?> GetStaffByEmailAsync(string email);
        Task<IEnumerable<Staff>> GetAllStaff();
        Task<Staff?> AddStaffAsync(Staff staff);
    }
}
