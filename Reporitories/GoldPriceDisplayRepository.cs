using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BackEnd.Reporitories
{
    public class GoldPriceDisplayRepository : IGoldPriceDisplayRepository
    {
        private readonly Banhang3Context _context;

        public GoldPriceDisplayRepository(Banhang3Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GoldPriceDisplay>> GetAllAsync()
        {
            return await _context.GoldPriceDisplays.ToListAsync();
        }

        public async Task<GoldPriceDisplay?> GetByIdAsync(int? id)
        {
            return await _context.GoldPriceDisplays.FindAsync(id);
        }

        public async Task AddAsync(GoldPriceDisplay goldPriceDisplay)
        {
            goldPriceDisplay.LastUpdated = DateTime.UtcNow;
            await _context.GoldPriceDisplays.AddAsync(goldPriceDisplay);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GoldPriceDisplay goldPriceDisplay)
        {
            goldPriceDisplay.LastUpdated = DateTime.UtcNow;
            _context.GoldPriceDisplays.Update(goldPriceDisplay);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.GoldPriceDisplays.FindAsync(id);
            if (entity != null)
            {
                _context.GoldPriceDisplays.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal?> GetGoldPriceByLocation(string? location)
        {
            var goldPriceDisplay = await _context.GoldPriceDisplays
           .FirstOrDefaultAsync(g => g.Location == location);

            return goldPriceDisplay?.GoldPrice;
        }
    }
}
