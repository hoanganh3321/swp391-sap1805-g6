using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Reporitories
{

    public class OrderRepositoty : IOrderRepository
    {
        private readonly Banhang3Context _context;

        public OrderRepositoty(Banhang3Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Order?> AddOrderAsyn(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> UpdateOrderAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return order;
        }
          public async Task<int?> SearchCustomer(int? customerId)
        {
            // Kiểm tra nếu customerId là null, trả về null ngay lập tức
            if (customerId == null)
            {
                return null;
            }

            // Tìm đơn hàng đầu tiên có CustomerID khớp với giá trị customerId
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.CustomerId == customerId);

            // Nếu tìm thấy đơn hàng, trả về CustomerID của đơn hàng đó, ngược lại trả về null
            return order?.CustomerId;
        }



        public async Task<int?> SearchOdId(int? customerId)
        {
            // Kiểm tra nếu customerId là null, trả về null ngay lập tức
            if (customerId == null)
            {
                return null;
            }

            // Tìm đơn hàng đầu tiên có CustomerID khớp với giá trị customerId
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.CustomerId == customerId);

            // Nếu tìm thấy đơn hàng, trả về OrderID, ngược lại trả về null
            return order?.OrderId;
        }

        public async Task<int?> GetOrderByIdAsync(int? customerId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.CustomerId == customerId);

            return order?.OrderId;
        }

        public async Task<Order?> GetOrderByIdAsync2(int? orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<decimal?> GetTotalAmountAsync(int? customerId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.CustomerId == customerId);
            return order?.TotalAmount;
        }

        public async Task DeleteOrderAsync(int? orderId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(od => od.OrderId == orderId);

            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
