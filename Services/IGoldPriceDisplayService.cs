using BackEnd.Models;

namespace BackEnd.Services
{
    public interface IGoldPriceDisplayService
    {
        Task<IEnumerable<GoldPriceDisplay>> GetAllAsync();
        Task<GoldPriceDisplay?> GetByIdAsync(int? id);
        Task AddAsync(GoldPriceDisplay goldPriceDisplay);
        Task UpdateAsync(GoldPriceDisplay goldPriceDisplay);
        Task DeleteAsync(int id);
    }
}
