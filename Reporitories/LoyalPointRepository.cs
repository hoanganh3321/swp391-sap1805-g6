﻿using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Reporitories
{
    public class LoyalPointRepository : ILoyalPointRepository
    {
        private readonly Banhang3Context _context;

        public LoyalPointRepository(Banhang3Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<LoyaltyPoint> AddLoyalPointAsync(LoyaltyPoint LoyalPoint)
        {
            _context.LoyaltyPoints.Add(LoyalPoint);
            await _context.SaveChangesAsync();
            return LoyalPoint;
        }

        public async Task<IEnumerable<LoyaltyPoint>> GetAllLoyalPointsAsync()
        {
            return await _context.LoyaltyPoints.ToListAsync();
        }

        public async Task<bool> UpdateLoyalPointAsync(int? customerId, LoyaltyPoint LoyalPoint)
        {
            var existingLoyalPoint = await _context.LoyaltyPoints.FirstOrDefaultAsync(o=>o.CustomerId==customerId);
            if (existingLoyalPoint == null)
            {
                return false;
            }
            existingLoyalPoint.CustomerId = LoyalPoint.CustomerId;
            existingLoyalPoint.Points = LoyalPoint.Points;
            existingLoyalPoint.LastUpdated = LoyalPoint.LastUpdated;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePointAsync(int ID)
        {
            var loyaltyPoint = await _context.LoyaltyPoints.FindAsync(ID);
            if (loyaltyPoint == null)
            {
                return false;
            }

            _context.LoyaltyPoints.Remove(loyaltyPoint);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<LoyaltyPoint?> GetByIdAsync(int id)
        {
            return await _context.LoyaltyPoints.FindAsync(id);
        }

        public async Task<LoyaltyPoint?> GetLoyalty(int customerId)
        {
            return await _context.LoyaltyPoints.FirstOrDefaultAsync(lp => lp.CustomerId == customerId);
        }
        public async Task<int?> GetLoyalIdByCustomerIdAsync(int customerId)
        {
            var loyalty = await _context.LoyaltyPoints.FirstOrDefaultAsync(o => o.CustomerId == customerId);
            return loyalty?.Id;
        }
        public async Task<int?> GetPoints(int? customerId)
        {
            var loyalty = await _context.LoyaltyPoints.FirstOrDefaultAsync(o => o.CustomerId == customerId);
            return loyalty?.Points;
        }
    }
}