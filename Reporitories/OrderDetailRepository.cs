using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Reporitories
{

    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly Banhang3Context _context;

        public OrderDetailRepository(Banhang3Context context)
        {
            _context = context;
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
           
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Order>> GetOrderDetailsByCusIdAsync(int? customerId)
        {
            return await _context.Orders
                                 .Where(od => od.CustomerId == customerId).Include(od=>od.OrderDetails)
                                 .Include(od => od.Customer).ThenInclude(od => od.LoyaltyPoints)
                                 .ToListAsync();
        }

        public async Task<List<OrderDetail>> GetOrderDetailsAsync(int? orderId)
        {
            return await _context.OrderDetails.Where(od => od.OrderId == orderId).ToListAsync();
        }
        public async Task DeleteOrderDetailAsync(int productId, int? orderId)
        {
            var orderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.ProductId == productId && od.OrderId == orderId);

            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> UpdateOrderDetail(OrderDetail orderDetail)
        {
          
            var existingOrderDetail = await _context.OrderDetails
            .FirstOrDefaultAsync(od => od.OrderId == orderDetail.OrderId 
            && od.ProductId == orderDetail.ProductId);
            if (existingOrderDetail == null)
            {
                return false;
            }
            existingOrderDetail.Quantity =  orderDetail.Quantity;
            existingOrderDetail.UnitPrice=  orderDetail.UnitPrice;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int?> FindPro(int productId, int? orderid)
        {
            if (orderid == null)
            {
                return null;
            }
            var orderDetail = await _context.OrderDetails
                                            .FirstOrDefaultAsync(od => od.ProductId == productId
                                            && od.OrderId == orderid);
            if (orderDetail == null)
            {
                return null;
            }
            return orderDetail.ProductId;


        }

        public async Task<decimal?> FindUni(int productId, int? orderId)
        {
            if (orderId == null)
            {
                return null;
            }
            var orderDetail = await _context.OrderDetails
                                            .FirstOrDefaultAsync(od => od.ProductId == productId 
                                            && od.OrderId == orderId);
            if (orderDetail == null)
            {
                return null;
            }
            return orderDetail.UnitPrice;
        }


        public async Task<int?> FindQuan(int productId, int? orderId)
        {
            var orderDetail = await _context.OrderDetails
                                        .FirstOrDefaultAsync(od => od.ProductId == productId 
                                        && od.OrderId == orderId);
            if (orderDetail == null)
            {
                return null;
            }
            return orderDetail.Quantity;
        }

        public async Task<int?> FindOdId(int? orderid)
        {
            return await _context.OrderDetails
                .Where(od => od.OrderId == orderid)
                .Select(od => od.OrderId)
                .FirstOrDefaultAsync();
        }
    }
}
