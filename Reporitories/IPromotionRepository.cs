using BackEnd.Models;
using BackEnd.ViewModels;

namespace BackEnd.Reporitories
{
    public interface IPromotionRepository
    {
        Task<IEnumerable<Promotion>> GetAllAsync();
        Task<Promotion?> GetByIdAsync(int? id);
        Task AddAsync(int? adminId, PromotionViewModel promotion);
        Task<bool> UpdateAsync(int? adminId,int promotionId, Promotion promotion);
        Task DeleteAsync(int id);
    }
}
