using BackEnd.Models;
using BackEnd.Reporitories;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services
{
    public class GoldPriceDisplayService : IGoldPriceDisplayService
    {
        private readonly IGoldPriceDisplayRepository _repository;

        public GoldPriceDisplayService(IGoldPriceDisplayRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GoldPriceDisplay>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<GoldPriceDisplay?> GetByIdAsync(int? id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(GoldPriceDisplay goldPriceDisplay)
        {
            await _repository.AddAsync(goldPriceDisplay);
        }

        public async Task UpdateAsync(GoldPriceDisplay goldPriceDisplay)
        {
            await _repository.UpdateAsync(goldPriceDisplay);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        
    }
}
