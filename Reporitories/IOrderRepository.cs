﻿using BackEnd.Models;

namespace BackEnd.Reporitories
{
    public interface IOrderRepository
    {
        Task<Order?> AddOrderAsyn(Order order);
       // Task<Order?> GetOrderByIdAsync(int orderId);
        Task<Order?> UpdateOrderAsync(Order order);
        Task<int?> SearchCustomer(int? customerId);
        Task<int?> SearchCustomerByOrderId(int? orderId);
        Task<int?> SearchOdId(int? customerId);
        Task<int?> GetOrderByIdAsync(int? customerId);

        Task<Order?> GetOrderByIdAsync2(int? orderId);

        Task<decimal?> GetTotalAmountAsync(int? customerId);
        Task<decimal?> GetPriceWithNoPromotionAsync(int? orderId);
        Task DeleteOrderAsync(int? orderId);
    }
}