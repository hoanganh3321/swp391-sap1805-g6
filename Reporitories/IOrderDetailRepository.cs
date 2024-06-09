using BackEnd.Models;

namespace BackEnd.Reporitories
{
    public interface IOrderDetailRepository
    {
        Task AddOrderDetailAsync(OrderDetail orderDetail);
        Task<IEnumerable<Order>> GetOrderDetailsByCusIdAsync(int? customerId);
        Task<List<OrderDetail>> GetOrderDetailsAsync(int? orderId);

        Task DeleteOrderDetailAsync(int productId, int? orderId);

        Task<bool> UpdateOrderDetail(OrderDetail orderDetail);

        Task<int?> FindPro(int productId,int? orderid);

        Task<decimal?> FindUni(int productId, int? orderId);
        Task<int?> FindQuan(int productId, int? orderId);

        Task<int?> FindOdId(int? orderid);
    }
}
