using BackEnd.Exceptions;
using BackEnd.Filters;
using BackEnd.Models;
using BackEnd.Reporitories;
using BackEnd.ViewModels;

namespace BackEnd.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IPromotionService _promotionService;
      


        public InvoiceService(IInvoiceRepository invoiceRepository,
          IOrderRepository orderRepository,
          ICustomerRepository customerRepository,
          IStaffRepository staffRepository,
          IStoreRepository storeRepository,
          IPromotionService promotionService)
        {
            _invoiceRepository = invoiceRepository;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _staffRepository = staffRepository;
            _storeRepository = storeRepository;
            _promotionService = promotionService;
            
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(int? orderId)
        {
            return await _invoiceRepository.GetInvoiceByIdAsync(orderId);
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _invoiceRepository.GetAllInvoicesAsync();
        }

        public async Task AddInvoiceAsync(int? orderId, int? staffId)
        {
            if (orderId == null)
            {
                throw new ArgumentNullException(nameof(orderId));
            }

            var priceWithNoPromotion = await _orderRepository.GetPriceWithNoPromotionAsync(orderId);
            if (priceWithNoPromotion == null)
            {
                throw new BadRequestException("Khách hàng chưa đặt hàng, vui lòng đặt hàng trước khi giao dịch được thực hiện");
            }

            var customerId = await _orderRepository.SearchCustomerByOrderId(orderId);
            if (customerId == null)
            {
                throw new BadRequestException("Khách hàng chưa đặt hàng, vui lòng đặt hàng trước khi giao dịch được thực hiện");
            }

            var loyalPointOfCustomer = await _customerRepository.GetCustomerLoyalPointByCustomerId1(customerId);
            int customerLoyaltyPoints = loyalPointOfCustomer?.Points ?? 0;

            var applicablePromotion = await GetApplicablePromotionAsync(customerLoyaltyPoints);
            if (applicablePromotion == null)
            {
                throw new InvalidOperationException("Current promotions are currently unavailable for your situation");
            }

            var totalPrice = CalculateTotalPrice(priceWithNoPromotion, applicablePromotion.Discount);

            var invoice = new Invoice()
            {
                OrderId = orderId.Value,
                PromotionId = applicablePromotion.PromotionId,
                PromotionName = applicablePromotion.Name,
                TotalPrice = totalPrice,
                StaffId = staffId
            };

            await _invoiceRepository.AddInvoiceAsync(invoice);

            // Cập nhật doanh thu của cửa hàng nơi nhân viên làm việc
            await UpdateStoreRevenueAsync(staffId);
        }

        private async Task<Promotion?> GetApplicablePromotionAsync(int customerLoyaltyPoints)
        {
            var promotions = await _promotionService.GetAllPromotionsAsync();
            return promotions
                .Where(p => customerLoyaltyPoints >= p.Points)
                .OrderByDescending(p => p.Points)
                .FirstOrDefault();
        }

        private decimal? CalculateTotalPrice(decimal? priceWithNoPromotion, decimal? discount)
        {
            return (priceWithNoPromotion * discount);
        }

        private async Task UpdateStoreRevenueAsync(int? staffId)
        {
            var invoices = await _invoiceRepository.GetInVoiceAsync(staffId);
            var store = await _staffRepository.GetStoreByStaffIdAsync(staffId);

            store.Revenue = invoices.Sum(invoice => invoice.TotalPrice);
            await _storeRepository.UpdateRevenStoreAsync(store);
        }



        public async Task<Invoice> UpdateInvoiceAsync(Invoice invoice)
        {
            return await _invoiceRepository.UpdateInvoiceAsync(invoice);
        }

        public async Task<bool> DeleteInvoiceAsync(int invoiceId)
        {
            return await _invoiceRepository.DeleteInvoiceAsync(invoiceId);
        }
    }
}
