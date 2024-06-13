using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BackEnd.Reporitories
{
    public class ProductReturnRepository : IProductReturnRepository

    {
        private readonly Banhang3Context _context;

        public ProductReturnRepository(Banhang3Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductReturn>> GetAllAsync()
        {
            return await _context.ProductReturns.ToListAsync();
        }

        public async Task<ProductReturn?> GetByIdAsync(int? productId)
        {
            return await _context.ProductReturns.FindAsync(productId);
        }

        public async Task AddAsync(ProductReturn productReturn)
        {
            _context.ProductReturns.Add(productReturn);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductReturn productReturn)
        {
            _context.ProductReturns.Update(productReturn);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId)
        {
            var productReturn = await _context.ProductReturns.FindAsync(productId);
            if (productReturn != null)
            {
                _context.ProductReturns.Remove(productReturn);
                await _context.SaveChangesAsync();
            }
        }
    }
}
