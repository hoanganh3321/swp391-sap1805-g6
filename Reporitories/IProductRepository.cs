using BackEnd.Models;

namespace BackEnd.Reporitories
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product product);
        Task<Product?> GetProductByIdAsync(int? productId);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<bool> UpdateProductAsync(int productId, Product product);
        Task<bool> DeleteProductAsync(int productId);
    }
}
