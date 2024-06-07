using Microsoft.EntityFrameworkCore;
using swp391_sap1805_g6.Entities;

namespace swp391_sap1805_g6.Reporitories
{
    public class OrderRepo
    {
        private static BanhangContext? _context;
       

        public static void Order(Order order)
        {
            _context = new BanhangContext();
            
            // Giả sử bạn muốn tìm Customer với CustomerId cụ thể (ví dụ: CustomerId = 1)
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);

            if (customer != null)
            {
                order.CustomerId = customer.CustomerId; // Gán CustomerId của Customer vào Order
                _context.Orders.Add(order); // Thêm Order vào DbSet
                _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
            else
            {
                // Xử lý trường hợp không tìm thấy Customer (ví dụ: ném ngoại lệ hoặc ghi log)
                throw new Exception("Customer not found.");
            }
        }

    }
}
