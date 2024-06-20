using BackEnd.Models;
using BackEnd.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace BackEnd.Reporitories
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly Banhang3Context _context;

        public PromotionRepository(Banhang3Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Promotion>> GetAllAsync()
        {
            return await _context.Promotions.ToListAsync();
        }

        public async Task<Promotion?> GetByIdAsync(int? id)
        {
            return await _context.Promotions.FindAsync(id);
        }

        public async Task AddAsync(int? adminId, PromotionViewModel promotion)
        {
            var promo = new Promotion
            {
                Name = promotion.Name,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate,
                Discount = promotion.Discount,
                Approved = promotion.Approved,
                Points= promotion.Points,
                ApprovedBy=adminId
            };
            _context.Promotions.Add(promo);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int? adminId,int promotionId, Promotion promotion)
        {
            var existingPromo = await _context.Promotions.FindAsync(promotionId);
            if (existingPromo == null)
            {
                return false;
            }
            existingPromo.Name = promotion.Name;
            existingPromo.StartDate = promotion.StartDate;
            existingPromo.StartDate = promotion.StartDate;
            existingPromo.Discount = promotion.Discount;
            existingPromo.Approved = promotion.Approved;
            existingPromo.ApprovedBy= adminId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion != null)
            {
                _context.Promotions.Remove(promotion);
                await _context.SaveChangesAsync();
            }
        }
    }
}
