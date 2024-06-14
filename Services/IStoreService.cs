using BackEnd.Models;

namespace BackEnd.Services
{
    public interface IStoreService
    {
        Task<IEnumerable<Store>> GetAllStoresAsync();
        Task<Store?> GetStoreByIdAsync(int id);
        Task<Store> CreateStoreAsync(Store store);
        Task<Store> UpdateStoreAsync(Store store);
        Task DeleteStoreAsync(int id);
    }
}
