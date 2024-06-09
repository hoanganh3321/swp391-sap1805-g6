using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Reporitories;
public class ProductRepository : IProductRepository
{
    private readonly Banhang3Context _context;

    public ProductRepository(Banhang3Context context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context)) ;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }


    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<bool> UpdateProductAsync(int productId, Product product)
    {
        var existingProduct = await _context.Products.FindAsync(productId);
        if (existingProduct == null)
        {
            return false;
        }

        existingProduct.ProductName = product.ProductName;
        existingProduct.Barcode = product.Barcode;
        existingProduct.Weight = product.Weight;
        existingProduct.Price = product.Price;
        existingProduct.ManufacturingCost = product.ManufacturingCost;
        existingProduct.StoneCost = product.StoneCost;
        existingProduct.Warranty = product.Warranty;
        existingProduct.Quantity = product.Quantity;
        existingProduct.IsBuyback = product.IsBuyback;
        existingProduct.CategoryId = product.CategoryId;
        existingProduct.StoreId = product.StoreId;
        existingProduct.Image= product.Image;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Product?> GetProductByIdAsync(int? productId)
    {
        return await _context.Products.FindAsync(productId);
    }
}
