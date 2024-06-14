using System.Drawing;
using System.Reflection;
using BackEnd.Extensions;
using BackEnd.Models;
using BackEnd.Reporitories;
using BackEnd.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BackEnd.Services
{
    public class LoyalPointService : ILoyalPointService
    {

        private readonly ILoyalPointRepository _repository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;


        public LoyalPointService(ILoyalPointRepository repository, 
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository)
        {
            _repository = repository;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<LoyaltyPoint>> GetAllAsync()
        {
            return await _repository.GetAllLoyalPointsAsync();
        }

        public async Task<LoyaltyPoint?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(OrderViewModel orderViewModel)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(orderViewModel.customerId);
            if (customer == null)
            {
                throw new Exception("Không tìm thấy khách hàng.");
            }

            var existingLoyalty = await _repository.GetLoyalty(orderViewModel.customerId);
            if (existingLoyalty == null)
            {
                decimal? total = await _orderRepository.GetTotalAmountAsync(orderViewModel.customerId);
                if (total >= 10000)
                {
                    var point = new LoyaltyPoint
                    {
                        Points = 10 * orderViewModel.Quantity,
                        CustomerId = orderViewModel.customerId,
                        LastUpdated = DateTime.UtcNow,
                    };
                    await _repository.AddLoyalPointAsync(point);
                }
            }
            else
            {
                var currentPoint = await _repository.GetPoints(orderViewModel.customerId);
                decimal? total = await _orderRepository.GetTotalAmountAsync(orderViewModel.customerId);
                if (total >= 10000)
                {
                    var point2 = new LoyaltyPoint
                    {
                        Points = currentPoint.Value + 10 * orderViewModel.Quantity,
                        CustomerId = orderViewModel.customerId,
                        LastUpdated = DateTime.UtcNow,
                    };
                    await _repository.UpdateLoyalPointAsync(orderViewModel.customerId, point2);
                }
            }
        }


        public async Task<bool> UpdateAsync(int id, LoyaltyPoint loyaltyPoint)
        {
            loyaltyPoint.LastUpdated = DateTime.UtcNow;
            return await _repository.UpdateLoyalPointAsync(id, loyaltyPoint);
        }

        public async Task DeleteAsync(StaffDeleteModel staffDeleteModel)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(staffDeleteModel.customerId);
            if (customer == null)
            {
                throw new Exception("Không tìm thấy khách hàng.");
            }
            var existingLoyalty = await _repository.GetLoyalty(staffDeleteModel.customerId);
            if (existingLoyalty == null)
            {
                throw new Exception("bạn chưa mua bất kỳ sản phẩm nào");
            }
            else
            {
                var currentPoint = await _repository.GetPoints(staffDeleteModel.customerId);
                decimal? total = await _orderRepository.GetTotalAmountAsync(staffDeleteModel.customerId);

                if (total >= 10000)
                {
                    var point2 = new LoyaltyPoint
                    {
                        Points = currentPoint.Value - 10 * staffDeleteModel.Quantity,
                        CustomerId = staffDeleteModel.customerId,
                        LastUpdated = DateTime.UtcNow,
                    };
                    await _repository.UpdateLoyalPointAsync(staffDeleteModel.customerId, point2);
                }
                var later = await _repository.GetPoints(staffDeleteModel.customerId);
                if (later == 0 || later == null)
                {
                    await _repository.DeletePointAsync(staffDeleteModel.customerId);
                }
            }
        }
    }
}
