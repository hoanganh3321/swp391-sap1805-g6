using BackEnd.Models;
using BackEnd.Reporitories;

namespace BackEnd.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        // Validate the product data before adding
        if (!ValidateProduct(product))
        {
            throw new ArgumentException("Product data is not valid.");
        }
       

        return await _productRepository.AddProductAsync(product);
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _productRepository.GetProductByIdAsync(productId);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }

    public async Task<bool> UpdateProductAsync(int productId, Product product)
    {
        // Validate the product data before updating
        if (!ValidateProduct(product))
        {
            throw new ArgumentException("Product data is not valid.");
        }

        return await _productRepository.UpdateProductAsync(productId, product);
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
        return await _productRepository.DeleteProductAsync(productId);
    }

    private bool ValidateProduct(Product product)
    {
        // Implement your validation logic here
        // For example:
        // - Check if required fields are not null or empty
        // - Check if numeric fields have valid values
        // - Check if relationships to other entities are valid

        // Return true if the product is valid, false otherwise
        return !string.IsNullOrWhiteSpace(product.ProductName);
    }
}

