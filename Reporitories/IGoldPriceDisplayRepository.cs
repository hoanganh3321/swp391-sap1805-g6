using BackEnd.Models;

namespace BackEnd.Reporitories
{
    public interface IGoldPriceDisplayRepository
    {
        Task<IEnumerable<GoldPriceDisplay>> GetAllAsync();
        Task<GoldPriceDisplay?> GetByIdAsync(int? id);
        Task AddAsync(GoldPriceDisplay goldPriceDisplay);
        Task UpdateAsync(GoldPriceDisplay goldPriceDisplay);
        Task DeleteAsync(int id);
        Task<decimal?> GetGoldPriceByLocation(string? location);
    }
}
