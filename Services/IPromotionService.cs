using BackEnd.Models;
using BackEnd.ViewModels;

namespace BackEnd.Services
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetAllPromotionsAsync();
        Task<Promotion?> GetPromotionByIdAsync(int? id);
        Task CreatePromotionAsync(int? adminId, PromotionViewModel promotion);
        Task UpdatePromotionAsync(int? adminId, int id, Promotion promotion);
        Task DeletePromotionAsync(int id);
    }
}
