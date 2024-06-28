using BackEnd.Models;
using BackEnd.Reporitories;
using BackEnd.ViewModels;

namespace BackEnd.Services
{
    public class ProductReturnService : IProductReturnService
    {
        private readonly IProductReturnRepository _productReturnRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGoldPriceDisplayRepository _goldPriceDisplayRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductReturnService(IProductReturnRepository productReturnRepository,
            IProductRepository productRepository,
            IGoldPriceDisplayRepository goldPriceDisplayRepository,
            IOrderRepository orderRepository)
        {
            _productReturnRepository = productReturnRepository;
            _productRepository = productRepository;
            _goldPriceDisplayRepository = goldPriceDisplayRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<ProductReturn>> GetAllProductReturnsAsync()
        {
            return await _productReturnRepository.GetAllAsync();
        }

        public async Task<ProductReturn?> GetProductReturnByIdAsync(int? productId)
        {
            return await _productReturnRepository.GetByIdAsync(productId);
        }

        public async Task<ProductReturnResult> CreateProductReturnAsync(ProductReturnViewModel productReturn)
        {
            var goldPrice = await _goldPriceDisplayRepository.GetGoldPriceByLocation(productReturn.location);
            var customer = await _orderRepository.SearchCustomer(productReturn.CustomerId);
            var product = await _productRepository.GetProductByIdAsync(productReturn.ProductId);

            if (customer == null && product == null)
            {
                return await HandleNewCustomerReturnAsync(productReturn, goldPrice);
            }
            else
            {
                return await HandleExistingCustomerReturnAsync(productReturn, goldPrice);
            }
        }

        private async Task<ProductReturnResult> HandleNewCustomerReturnAsync(ProductReturnViewModel productReturn, decimal? goldPrice)
        {
            Product newProduct = new()
            {
                ProductName = productReturn.ProductName,
                Weight = productReturn.Weight,
                Price = productReturn.CategoryId == 21 ? productReturn.StoneCost : goldPrice,
                Quantity = productReturn.Quantity,
                IsBuyback = true,
                CategoryId = productReturn.CategoryId,
                StoreId = productReturn.StoreId,
                Image = productReturn.Image,
            };

            await _productRepository.AddProductAsync(newProduct);

            return new ProductReturnResult
            {
                IsNewProduct = true,
                Product = newProduct
            };
        }

        private async Task<ProductReturnResult> HandleExistingCustomerReturnAsync(ProductReturnViewModel productReturn, decimal? goldPrice)
        {
            ProductReturn returnPro = new()
            {
                ProductId = productReturn.ProductId,
                CustomerId = productReturn.CustomerId,
                ReturnDate = productReturn.ReturnDate,
                ReturnReason = productReturn.ReturnReason,
            };

            await _productReturnRepository.AddAsync(returnPro);

            Product buyback = new()
            {
                ProductName = productReturn.ProductName,
                Weight = productReturn.Weight,
                Price = productReturn.CategoryId == 21 ? (productReturn.Price * 0.7m) : goldPrice,
                Quantity = productReturn.Quantity,
                IsBuyback = true,
                CategoryId = productReturn.CategoryId,
                StoreId = productReturn.StoreId,
                Image = productReturn.Image,
            };

            await _productRepository.AddProductAsync(buyback);

            return new ProductReturnResult
            {
                IsNewProduct = false,
                ProductReturn = returnPro
            };
        }



        public async Task UpdateProductReturnAsync(ProductReturn productReturn)
        {
            await _productReturnRepository.UpdateAsync(productReturn);
        }

        public async Task DeleteProductReturnAsync(int productId)
        {
            await _productReturnRepository.DeleteAsync(productId);
        }
    }
}
