using BackEnd.Models;
using BackEnd.ViewModels;

namespace BackEnd.Services
{
    public interface IOrderDetailService
    {
        
        Task AddToCartAsync(AddToCartModel model);
        Task<IEnumerable<Order>> ViewCartAsync(int? customerId);
        Task DeleteFromCartAsync(DeleteCartViewModel deleteCartView);
        Task StaffAddToCartAsync(OrderViewModel cartmodel);
        Task StaffDeleteFromCartAsync(StaffDeleteModel deleteCartView);
        Task<IEnumerable<Order>> StaffViewCartAsync(int? customerId);
    }
}
