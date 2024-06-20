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

            // Lấy giá trị không đồng bộ của PriceWithNoPromotion
            var priceWithNoPromotion = await _orderRepository.GetPriceWithNoPromotionAsync(orderId);
            // Lấy giá trị không đồng bộ của customerId
            var customerId = await _orderRepository.SearchCustomerByOrderId(orderId);
            // Chờ để hoàn thành các nhiệm vụ không đồng bộ
            if (customerId == null)
            {
                throw new InvalidOperationException("Customer not found for the given order.");
            }

            // Lấy giá trị không đồng bộ của loyalty points
            var loyalPointOfCustomer = await _customerRepository.GetCustomerLoyalPointByCustomerId1(customerId);
            if (loyalPointOfCustomer == null)
            {
                throw new InvalidOperationException("Customer not found for the given order.");
            }
            // Lấy điểm khách hàng từ đối tượng loyalPointOfCustomer
            int? customerLoyaltyPoints = loyalPointOfCustomer.Points;
            //liet ke tat ca chinh sach khuyến mại đã có trong database
            var pro = await _promotionService.GetAllPromotionsAsync();
            // Tìm khuyến mại phù hợp với điểm khách hàng
            var applicablePromotion = pro
                .Where(p => customerLoyaltyPoints >= p.Points)
                .OrderByDescending(p => p.Points)
                .FirstOrDefault();

            if (applicablePromotion == null)
            {
                throw new InvalidOperationException("Customer does not have enough loyalty points for any promotion.");
            }

            // Tính toán giá trị cuối cùng dựa trên khuyến mại
            var totalPrice = priceWithNoPromotion * applicablePromotion.Discount;

            var invoice = new Invoice()
            {
                OrderId = orderId.Value,
                PromotionId = applicablePromotion.PromotionId,
                PromotionName = applicablePromotion.Name,
                TotalPrice = totalPrice,
                StaffId = staffId 
            };

            await _invoiceRepository.AddInvoiceAsync(invoice);
            // cập nhật lại doanh thu của cửa hàng nơi mà staff làm việc đặt hàng
            //
            // Lấy giá trị không đồng bộ của iv
            var iv = await _invoiceRepository.GetInVoiceAsync(staffId);
            // Lấy giá trị không đồng bộ của st
            var st = await _staffRepository.GetStoreByStaffIdAsync(staffId);
            //
            //update revenue store
            st.Revenue = 0;
            foreach (var item in iv) 
            {         
                st.Revenue += item.TotalPrice;   
            }
            await _storeRepository.UpdateRevenStoreAsync(st);
            
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
