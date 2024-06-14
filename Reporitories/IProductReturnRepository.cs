using BackEnd.Models;

namespace BackEnd.Reporitories
{
    public interface IProductReturnRepository
    {
        Task<IEnumerable<ProductReturn>> GetAllAsync();
        Task<ProductReturn?> GetByIdAsync(int? productId);
        Task AddAsync(ProductReturn productReturn);
        Task UpdateAsync(ProductReturn productReturn);
        Task DeleteAsync(int productId);
    }
}
