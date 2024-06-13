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
            var GoldPrice = await _goldPriceDisplayRepository.GetGoldPriceByLocation(productReturn.location);//require location
            var customer = await _orderRepository.SearchCustomer(productReturn.CustomerId);//require customerId
            var product = await _productRepository.GetProductByIdAsync(productReturn.ProductId);//require ProductId
            //neu customer == null && product == null thi day la khach hang moi
            if (customer == null && product == null)
            {//san pham mua lai tu khach hang moi

                if (productReturn.CategoryId == 21)
                {//neu la da quy thi chi mua lai da
                    Product newProduct = new()
                    {
                        ProductName = productReturn.ProductName,//require ProductName
                        Barcode = productReturn.Barcode,
                        Weight = productReturn.Weight,//require Weight
                        Price = productReturn.StoneCost,//require StoneCost
                        Warranty = productReturn.Warranty,
                        Quantity = productReturn.Quantity, //require Quantity
                        IsBuyback = true,
                        CategoryId = productReturn.CategoryId, //require CategoryId
                        StoreId = productReturn.StoreId,
                        Image = productReturn.Image, //require Image
                    };

                    await _productRepository.AddProductAsync(newProduct);
                    //ket qua 1
                    return new ProductReturnResult
                    {
                        IsNewProduct = true,
                        Product = newProduct
                    };
                }
                else  //neu customer != null && product != null thi day la khach hang cu

                { //neu la vang chi mua lai voi gia vang hien tai duoc cap nhat hang ngay
                    Product newProduct = new()
                    {
                        ProductName = productReturn.ProductName,
                        Barcode = productReturn.Barcode,
                        Weight = productReturn.Weight,
                        Price = GoldPrice,
                        Warranty = productReturn.Warranty,
                        Quantity = productReturn.Quantity,
                        IsBuyback = true,
                        CategoryId = productReturn.CategoryId,
                        StoreId = productReturn.StoreId,
                        Image = productReturn.Image,
                    };

                    await _productRepository.AddProductAsync(newProduct);
                    //ket qua 1
                    return new ProductReturnResult
                    {
                        IsNewProduct = true,
                        Product = newProduct
                    };
                }         
            }
            else
            {//san pham mua lai tu san pham da ban cho khach
                ProductReturn returnPro = new()
                {
                    ProductId = productReturn.ProductId,
                    CustomerId = productReturn.CustomerId,
                    ReturnDate = productReturn.ReturnDate,
                    ReturnReason = productReturn.ReturnReason,
                };

                await _productReturnRepository.AddAsync(returnPro);
                if (productReturn.CategoryId == 21)
                {//neu la da quy thi se mua lai =70% gia da ban
                    Product buyback = new()
                    {
                        ProductName = productReturn.ProductName,
                        Barcode = productReturn.Barcode,
                        Weight = productReturn.Weight,
                        Price = (productReturn.Price * 7) / 10,
                        Warranty = productReturn.Warranty,
                        Quantity = productReturn.Quantity,
                        IsBuyback = true,
                        CategoryId = productReturn.CategoryId,
                        StoreId = productReturn.StoreId,
                        Image = productReturn.Image,
                    };
                    await _productRepository.AddProductAsync(buyback);
                }else 
                {// chi mua lai phan vang thuc te theo gia vang dc cap nhat hang ngay
                    Product buyback = new()
                    {
                        ProductName = productReturn.ProductName,
                        Barcode = productReturn.Barcode,
                        Weight = productReturn.Weight,
                        Price = GoldPrice,
                        Warranty = productReturn.Warranty,
                        Quantity = productReturn.Quantity,
                        IsBuyback = true,
                        CategoryId = productReturn.CategoryId,
                        StoreId = productReturn.StoreId,
                        Image = productReturn.Image,
                    };
                    await _productRepository.AddProductAsync(buyback);
                }
              
            //ket qua 2
                return new ProductReturnResult
                {
                    IsNewProduct = false,
                    ProductReturn = returnPro
                };
            }
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
