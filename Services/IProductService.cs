using BackEnd.Models;

namespace BackEnd.Services
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(Product product);
        Task<Product?> GetProductByIdAsync(int productId);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<bool> UpdateProductAsync(int productId, Product product);
        Task<bool> DeleteProductAsync(int productId);
    }
}
