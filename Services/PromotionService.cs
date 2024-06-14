using BackEnd.Models;
using BackEnd.Reporitories;
using BackEnd.ViewModels;

namespace BackEnd.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task<IEnumerable<Promotion>> GetAllPromotionsAsync()
        {
            return await _promotionRepository.GetAllAsync();
        }

        public async Task<Promotion?> GetPromotionByIdAsync(int? id)
        {
            return await _promotionRepository.GetByIdAsync(id);
        }

        public async Task CreatePromotionAsync(int? adminId, PromotionViewModel promotion)
        {
            await _promotionRepository.AddAsync(adminId,promotion);
        }

        public async Task UpdatePromotionAsync(int? adminId,int id,Promotion promotion)
        {
            await _promotionRepository.UpdateAsync(adminId,id, promotion);
        }

        public async Task DeletePromotionAsync(int id)
        {
            await _promotionRepository.DeleteAsync(id);
        }
    }
}
