using BackEnd.Models;
using BackEnd.Reporitories;

namespace BackEnd.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<IEnumerable<Store>> GetAllStoresAsync()
        {
            return await _storeRepository.GetAllAsync();
        }

        public async Task<Store?> GetStoreByIdAsync(int id)
        {
            return await _storeRepository.GetByIdAsync(id);
        }

        public async Task<Store> CreateStoreAsync(Store store)
        {
            return await _storeRepository.AddAsync(store);
        }

        public async Task<Store> UpdateStoreAsync(Store store)
        {
            return await _storeRepository.UpdateAsync(store);
        }

        public async Task DeleteStoreAsync(int id)
        {
            await _storeRepository.DeleteAsync(id);
        }
    }
}
