using BackEnd.Models;
using BackEnd.ViewModels;

namespace BackEnd.Services
{
    public interface ILoyalPointService
    {
        Task<IEnumerable<LoyaltyPoint>> GetAllAsync();
        Task<LoyaltyPoint?> GetByIdAsync(int id);
        Task AddAsync(OrderViewModel orderViewModel);
        Task<bool> UpdateAsync(int id, LoyaltyPoint loyaltyPoint);
        Task DeleteAsync(StaffDeleteModel staffDeleteModel);
    }
}
