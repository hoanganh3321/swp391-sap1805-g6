using BackEnd.Models;

namespace BackEnd.Reporitories
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetAllAsync();
        Task<Store?> GetByIdAsync(int id);
        Task<Store> AddAsync(Store store);
        Task<Store> UpdateAsync(Store store);
        Task DeleteAsync(int id);
        Task<bool> UpdateRevenStoreAsync(Store store);
    }
}
