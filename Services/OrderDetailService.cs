using System.Security.Claims;
using BackEnd.Extensions;
using BackEnd.Models;
using BackEnd.Reporitories;
using BackEnd.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace BackEnd.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        
        public OrderDetailService(
             IOrderDetailRepository orderDetailRepository,
             IProductRepository productRepository,
             IOrderRepository orderRepository,
             IHttpContextAccessor httpContextAccessor)             
        {
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _httpContextAccessor = httpContextAccessor;

        }
        ////////////////////////////////////////////////////////////////Customer///////////////////////////////////////////
        //add to cart
        public async Task AddToCartAsync(AddToCartModel cartmodel)
        {
            Order newOrder = new Order();
                
            // Validate product
            var product = await _productRepository.GetProductByIdAsync(cartmodel.ProductID);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            if (product.Quantity < cartmodel.Quantity)
            {
                throw new Exception("Not enough stock");
            }

            // Get customer ID from HttpContext
            var currentCus = _httpContextAccessor.HttpContext.GetCustomerId();
            if (currentCus == null)
            {
                throw new InvalidOperationException("phiên đăng nhập đã hết hạn");
            }
            //check current customer
            var existingOrderTask = _orderRepository.SearchCustomer(currentCus);
            var existingOrder = await existingOrderTask;
            if (existingOrder!= currentCus)
            {
                // If there is no existing order, create a new one              
                newOrder.CustomerId = currentCus;
                newOrder.OrderDate = DateTime.Now;
               
                // Add the new order to the repository
                await _orderRepository.AddOrderAsyn(newOrder);

                // Save the OrderID to HttpContext
                _httpContextAccessor.HttpContext.Items["OrderID"] = newOrder.OrderId;
            }
            else
            {
               var acc =  await _orderRepository.GetOrderByIdAsync(currentCus);
                _httpContextAccessor.HttpContext.Items["OrderID"] = acc;
            }
            // Get the OrderID from HttpContext
            var orderId = _httpContextAccessor.HttpContext.GetOrderId();
            // Create order detail
            decimal? uni = await _orderDetailRepository.FindUni(cartmodel.ProductID,orderId);
            int? quan = await _orderDetailRepository.FindQuan(cartmodel.ProductID,orderId);
            int? odid = await _orderDetailRepository.FindOdId(orderId);
            //
            var findProResult = await _orderDetailRepository.FindPro(cartmodel.ProductID,orderId);           
                if (findProResult == cartmodel.ProductID
                    && odid == orderId)

                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = orderId,
                        ProductId = cartmodel.ProductID,
                        Quantity = (cartmodel.Quantity) + quan,
                        UnitPrice = uni + (product.Price * cartmodel.Quantity)
                    };
                    await _orderDetailRepository.UpdateOrderDetail(orderDetail);
                }         
            else
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = cartmodel.ProductID,
                    Quantity = cartmodel.Quantity,
                    UnitPrice = product.Price * cartmodel.Quantity
                };
                // Add order detail to repository
                await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
            }
            // Update order total amount and order date
            var dkm = await _orderRepository.GetOrderByIdAsync2(orderId);
            if (dkm != null)
            {
                var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync(orderId);
                if (orderDetails.Any())
                {
                    dkm.TotalAmount = 0; // Khởi tạo TotalAmount
                    foreach (var detail in orderDetails)
                    {
                        dkm.TotalAmount += detail.UnitPrice;
                    }
                    // Set order date
                    dkm.OrderDate = DateTime.Now;

                    // Update order total price
                    await _orderRepository.UpdateOrderAsync(dkm);
                }
            }
            else
            {
                throw new Exception("Order not found");
            }
            // Update product quantity
            product.Quantity -= cartmodel.Quantity;
            await _productRepository.UpdateProductAsync(product.ProductId, product);

        }

        //view cart
        public async Task<IEnumerable<Order>> ViewCartAsync(int? customerId)
        {
            return await _orderDetailRepository.GetOrderDetailsByCusIdAsync(customerId);
        }

        //delete from cart
        public async Task DeleteFromCartAsync(DeleteCartViewModel deleteCartView)
        {
            // Validate product
            var product = await _productRepository.GetProductByIdAsync(deleteCartView.ProductID);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            // Get customer ID from HttpContext
            var currentCus = _httpContextAccessor.HttpContext.GetCustomerId();
            if (currentCus == null)
            {
                throw new InvalidOperationException("phiên đăng nhập đã hết hạn");
            }
            //check current customer
            var existingOrderTask = _orderRepository.SearchCustomer(currentCus);
            var existingOrder = await existingOrderTask;
            if (existingOrder != currentCus)
            {
                throw new Exception("phiên đăng nhập đã hết hạn");
            }
            else
            {
                //set orderId of currentcus
                var acc = await _orderRepository.GetOrderByIdAsync(currentCus);
                _httpContextAccessor.HttpContext.Items["OrderID"] = acc;
                // Get the OrderID from HttpContext
                var orderId = _httpContextAccessor.HttpContext.GetOrderId();
                //update orderDetail
                decimal? uni = await _orderDetailRepository.FindUni(deleteCartView.ProductID,orderId);
                int? quan = await _orderDetailRepository.FindQuan(deleteCartView.ProductID,orderId);
                if (quan - (deleteCartView.Quantity) == 0)
                {
                    await _orderDetailRepository.DeleteOrderDetailAsync(deleteCartView.ProductID, orderId);
                    // Update product quantity
                    product.Quantity += deleteCartView.Quantity;
                    await _productRepository.UpdateProductAsync(product.ProductId, product);
                }
                else if (quan - (deleteCartView.Quantity) < 0)
                {
                    throw new Exception($"sản phẩm với mã {deleteCartView.ProductID} " +
                        $"hiện có số lượng tại rỏ là {quan} không đủ để xóa hãy đặt hêm");
                }
                var orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = deleteCartView.ProductID,
                    Quantity = quan - (deleteCartView.Quantity),
                    UnitPrice = uni- (product.Price * deleteCartView.Quantity)
                };
                //update orderDetail
                await _orderDetailRepository.UpdateOrderDetail(orderDetail);
                // Update order total amount and order date
                var dkm = await _orderRepository.GetOrderByIdAsync2(orderId);
                if (dkm != null)
                {
                    var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync(orderId);
                    if (orderDetails.Any())
                    {
                        dkm.TotalAmount = 0; // Khởi tạo TotalAmount
                        foreach (var detail in orderDetails)
                        {
                            dkm.TotalAmount += detail.UnitPrice;
                        }
                        // Set order date
                        dkm.OrderDate = DateTime.Now;

                        // Update order total price
                        await _orderRepository.UpdateOrderAsync(dkm);
                    }
                }
                else
                {
                    throw new Exception("Order not found");
                }
                // Update product quantity
                product.Quantity += deleteCartView.Quantity;
                await _productRepository.UpdateProductAsync(product.ProductId, product);
            }
        }

        ////////////////////////////////////////////////////////////////Staff//////////////////////////////////////////////

        //add
        public async Task StaffAddToCartAsync(OrderViewModel cartmodel)
        {
            Order newOrder = new Order();

            // Validate product
            var product = await _productRepository.GetProductByIdAsync(cartmodel.ProductID);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            if (product.Quantity < cartmodel.Quantity)
            {
                throw new Exception("Not enough stock");
            }

            // Get customer ID from HttpContext
            var currentStaff = _httpContextAccessor.HttpContext.GetStaffId();
            if (currentStaff == null)
            {
                throw new InvalidOperationException("phiên đăng nhập đã hết hạn");
            }


            //check current customer
            var existingOrderTask = _orderRepository.SearchCustomer(cartmodel.customerId);
            var existingOrder = await existingOrderTask;
            if (existingOrder == null)
            {
                // If there is no existing order, create a new one              
                newOrder.CustomerId = cartmodel.customerId;
                newOrder.OrderDate = DateTime.Now;

                // Add the new order to the repository
                await _orderRepository.AddOrderAsyn(newOrder);

                // Save the OrderID to HttpContext
                _httpContextAccessor.HttpContext.Items["OrderID"] = newOrder.OrderId;
            }
            else
            {
                var acc = await _orderRepository.GetOrderByIdAsync(cartmodel.customerId);
                _httpContextAccessor.HttpContext.Items["OrderID"] = acc;
            }
            // Get the OrderID from HttpContext
            var orderId = _httpContextAccessor.HttpContext.GetOrderId();
            // Create order detail
            decimal? uni = await _orderDetailRepository.FindUni(cartmodel.ProductID, orderId);
            int? quan = await _orderDetailRepository.FindQuan(cartmodel.ProductID, orderId);
            int? odid = await _orderDetailRepository.FindOdId(orderId);
            // check xem khách hàng hiện tại đã mua hàng lần nào chưa 
            var findProResult = await _orderDetailRepository.FindPro(cartmodel.ProductID, orderId);
            if (findProResult == cartmodel.ProductID
                && odid == orderId)//nếu rồi

            {
                var orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = cartmodel.ProductID,
                    Quantity = (cartmodel.Quantity) + quan,
                    UnitPrice = uni + (product.Price * cartmodel.Quantity)
                };
                //gọi hàm update
                await _orderDetailRepository.UpdateOrderDetail(orderDetail);
            }
            else
            {// nếu chưa
                var orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = cartmodel.ProductID,
                    Quantity = cartmodel.Quantity,
                    UnitPrice = product.Price * cartmodel.Quantity
                };
                // gọi hàm add
                await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
            }
            // Update order total amount and order date
            var dkm = await _orderRepository.GetOrderByIdAsync2(orderId);
            if (dkm != null)
            {
                var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync(orderId);
                if (orderDetails.Any())
                {
                    dkm.TotalAmount = 0; // Khởi tạo TotalAmount
                    foreach (var detail in orderDetails)
                    {
                        dkm.TotalAmount += detail.UnitPrice;
                    }
                    // Set order date
                    dkm.OrderDate = DateTime.Now;

                    // Update order total price
                    await _orderRepository.UpdateOrderAsync(dkm);
                }
            }
            else
            {
                throw new Exception("Order not found");
            }
            // Update product quantity
            product.Quantity -= cartmodel.Quantity;
            await _productRepository.UpdateProductAsync(product.ProductId, product);
        }

        //delete from cart
        public async Task StaffDeleteFromCartAsync(StaffDeleteModel deleteCartView)
        {

            // Validate product
            var product = await _productRepository.GetProductByIdAsync(deleteCartView.ProductID);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            // Get staff ID from HttpContext
            var currentStaff = _httpContextAccessor.HttpContext.GetStaffId();
            if (currentStaff == null)
            {
                throw new InvalidOperationException("phiên đăng nhập đã hết hạn");
            }

            //check current customer
            var existingOrderTask = _orderRepository.SearchCustomer(deleteCartView.customerId);
            var existingOrder = await existingOrderTask;
            if (existingOrder == null)
            {
                throw new Exception("bạn chưa đặt hàng");
            }
            else
            {
                //set orderId of currentcus
                var acc = await _orderRepository.GetOrderByIdAsync(deleteCartView.customerId);
                _httpContextAccessor.HttpContext.Items["OrderID"] = acc;

                // Get the OrderID from HttpContext
                var orderId = _httpContextAccessor.HttpContext.GetOrderId();

                // Update orderDetail
                decimal? uni = await _orderDetailRepository.FindUni(deleteCartView.ProductID, orderId);
                int? quan = await _orderDetailRepository.FindQuan(deleteCartView.ProductID, orderId);
                if (quan == null) { quan = 0; }
                var dkm = await _orderRepository.GetOrderByIdAsync2(orderId);
//
                if (quan - (deleteCartView.Quantity) == 0)
                {
                    // Xóa chi tiết đơn hàng
                    await _orderDetailRepository.DeleteOrderDetailAsync(deleteCartView.ProductID, orderId);

                    // Kiểm tra xem có chi tiết đơn hàng nào khác còn lại không
                    var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync(orderId);

                    if (dkm != null)
                    {
                        if (orderDetails.Any())
                        {
                            // Cập nhật tổng số tiền của đơn hàng
                            dkm.TotalAmount = orderDetails.Sum(detail => detail.UnitPrice);

                            // Set order date
                            dkm.OrderDate = DateTime.Now;

                            // Update order total price
                            await _orderRepository.UpdateOrderAsync(dkm);
                        }
                        else
                        {
                            // Xóa đơn hàng nếu không còn chi tiết đơn hàng nào khác
                           await _orderRepository.DeleteOrderAsync(orderId);
                        }
                    }
                    // Cập nhật số lượng sản phẩm
                    product.Quantity += deleteCartView.Quantity;
                    await _productRepository.UpdateProductAsync(product.ProductId, product);
                }
//
                else if (quan - (deleteCartView.Quantity) < 0)
                {
                    throw new Exception($"sản phẩm với mã {deleteCartView.ProductID} " +
                        $"hiện có số lượng tại giỏ là {quan} không đủ để xóa hãy đặt hêm");
                }
//
                else
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = orderId,
                        ProductId = deleteCartView.ProductID,
                        Quantity = quan - (deleteCartView.Quantity),
                        UnitPrice = uni - (product.Price * deleteCartView.Quantity)
                    };
                    //update orderDetail
                    await _orderDetailRepository.UpdateOrderDetail(orderDetail);
                    // Update order total amount and order date
                    var dcm = await _orderRepository.GetOrderByIdAsync2(orderId);
                    if (dcm != null)
                    {
                        var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync(orderId);
                        if (orderDetails.Any())
                        {
                            dcm.TotalAmount = 0; // Khởi tạo TotalAmount
                            foreach (var detail in orderDetails)
                            {
                                dcm.TotalAmount += detail.UnitPrice;
                            }
                            // Set order date
                            dcm.OrderDate = DateTime.Now;

                            // Update order total price
                            await _orderRepository.UpdateOrderAsync(dcm);
                        }
                    }
                    else
                    {
                        throw new Exception("Order not found");
                    }
                    // Update product quantity
                    product.Quantity += deleteCartView.Quantity;
                    await _productRepository.UpdateProductAsync(product.ProductId, product);
                }
            }
        }

        public async Task<IEnumerable<Order>> StaffViewCartAsync(int? customerId)
        {
            return await _orderDetailRepository.GetOrderDetailsByCusIdAsync(customerId);
        }

    }
}














