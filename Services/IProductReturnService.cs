using BackEnd.Models;
using BackEnd.ViewModels;

namespace BackEnd.Services
{
    public interface IProductReturnService
    {
        Task<IEnumerable<ProductReturn>> GetAllProductReturnsAsync();
        Task<ProductReturn?> GetProductReturnByIdAsync(int? productId);
        Task<ProductReturnResult> CreateProductReturnAsync(ProductReturnViewModel productReturn);
        Task UpdateProductReturnAsync(ProductReturn productReturn);
        Task DeleteProductReturnAsync(int productId);
    }
}
