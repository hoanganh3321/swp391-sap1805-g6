using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Reporitories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly Banhang3Context _context;

        public StoreRepository(Banhang3Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Store>> GetAllAsync()
        {
            return await _context.Set<Store>().Include(od=>od.Staff).ThenInclude(od=>od.Invoices).ToListAsync();
        }

        public async Task<Store?> GetByIdAsync(int id)
        {
            return await _context.Set<Store>().FindAsync(id);
        }

        public async Task<Store> AddAsync(Store store)
        {
            _context.Set<Store>().Add(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task<Store> UpdateAsync(Store store)
        {
            _context.Entry(store).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return store;
        }
        public async Task<bool> UpdateRevenStoreAsync(Store store)
        {
            var existingStore = await _context.Stores.FindAsync(store.StoreId);
            if (existingStore == null)
            {
                return false;
            }

            existingStore.Revenue = store.Revenue;
            

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var store = await GetByIdAsync(id);
            if (store != null)
            {
                _context.Set<Store>().Remove(store);
                await _context.SaveChangesAsync();
            }
        }
    }
}
