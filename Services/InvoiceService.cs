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
            //
            // Lấy giá trị không đồng bộ của PriceWithNoPromotion
            var priceWithNoPromotion = await _orderRepository.GetPriceWithNoPromotionAsync(orderId);
            if (priceWithNoPromotion == null) { throw new BadRequestException("Khách hàng chưa đặt hàng, vui lòng đặt hàng trước khi giao dịch được thực hiện"); }
            // Lấy giá trị không đồng bộ của customerId
            var customerId = await _orderRepository.SearchCustomerByOrderId(orderId);
            if (customerId == null) { throw new BadRequestException("Khách hàng chưa đặt hàng, vui lòng đặt hàng trước khi giao dịch được thực hiện"); }
            // Chờ để hoàn thành các nhiệm vụ không đồng bộ
            if (customerId == null)
            {
                throw new InvalidOperationException("Customer not found for the given order.");
            }
            ///
            // Lấy giá trị không đồng bộ của loyalty points
            var ContiOrder = await _invoiceRepository.GetOldInvoiceByOrderAsync(orderId);
            var loyalPointOfCustomer = await _customerRepository.GetCustomerLoyalPointByCustomerId1(customerId);
            //
            if (ContiOrder == null) 
            {
                //KHÁC HÀNG CHƯA CÓ HÓA ĐƠN TRƯỚC ĐÓ : KHÁCH HÀNG LẦN ĐẦU TỚI MUA HÀNG
                if (loyalPointOfCustomer == null)
                {
                    int? customerLoyaltyPoints = 0;
                    //liet ke tat ca chinh sach khuyến mại đã có trong database
                    var pro1 = await _promotionService.GetAllPromotionsAsync();
                    // Tìm khuyến mại phù hợp với điểm khách hàng
                    var applicablePromotion1 = pro1
                        .Where(p => customerLoyaltyPoints >= p.Points)
                        .OrderByDescending(p => p.Points)
                        .FirstOrDefault();
                    //
                    if (applicablePromotion1 == null)
                    {
                        throw new InvalidOperationException("Current promotions are currently unavailable for your situation");
                    }
                    //
                    var invoicewithNoDiscount = new Invoice()
                    {
                        OrderId = orderId.Value,
                        PromotionId = applicablePromotion1.PromotionId,
                        PromotionName = applicablePromotion1.Name,
                        TotalPrice = priceWithNoPromotion,
                        StaffId = staffId
                    };
                    //
                    await _invoiceRepository.AddInvoiceAsync(invoicewithNoDiscount);
                }
                else
                {
                    //liet ke tat ca chinh sach khuyến mại đã có trong database
                    var pro = await _promotionService.GetAllPromotionsAsync();
                    // Tìm khuyến mại phù hợp với điểm khách hàng
                    var applicablePromotion = pro
                        .Where(p => loyalPointOfCustomer.Points >= p.Points)
                        .OrderByDescending(p => p.Points)
                        .FirstOrDefault();
                    //
                    if (applicablePromotion == null)
                    {
                        throw new InvalidOperationException("Current promotions are currently unavailable for your situation");
                    }
                    //
                    // Tính toán giá trị cuối cùng dựa trên khuyến mại
                    var totalPrice = priceWithNoPromotion * applicablePromotion.Discount;
                    //
                    var invoice = new Invoice()
                    {
                        OrderId = orderId.Value,
                        PromotionId = applicablePromotion.PromotionId,
                        PromotionName = applicablePromotion.Name,
                        TotalPrice = totalPrice,
                        StaffId = staffId
                    };
                    //
                    await _invoiceRepository.AddInvoiceAsync(invoice);
                }
            }
            else
            {
              //KHÁC HÀNG ĐÃ CÓ HÓA ĐƠN TRƯỚC ĐÓ : KHÁCH HÀNG TỚI MUA LẦN 2
                if (loyalPointOfCustomer == null)
                {
                    int? customerLoyaltyPoints = 0;
                    // Liệt kê tất cả chính sách khuyến mại đã có trong database
                    var pro1 = await _promotionService.GetAllPromotionsAsync();
                    // Tìm khuyến mại phù hợp với điểm khách hàng
                    var applicablePromotion1 = pro1
                        .Where(p => customerLoyaltyPoints >= p.Points)
                        .OrderByDescending(p => p.Points)
                        .FirstOrDefault();
                    //
                    if (applicablePromotion1 == null)
                    {
                        throw new InvalidOperationException("Current promotions are currently unavailable for your situation");
                    }
                    //
                    // Tính toán giá trị mới cho TotalPrice dựa trên khuyến mại
                    var totalPrice = priceWithNoPromotion * applicablePromotion1.Discount;

                    // Tạo hóa đơn mới
                    var newInvoice = new Invoice()
                    {
                        OrderId = orderId.Value,
                        PromotionId = applicablePromotion1.PromotionId,
                        PromotionName = applicablePromotion1.Name,
                        TotalPrice = totalPrice,
                        StaffId = staffId
                    };

                    // Lưu hóa đơn mới vào cơ sở dữ liệu
                    await _invoiceRepository.AddInvoiceAsync(newInvoice);
                }
                else
                {
                    // Liệt kê tất cả chính sách khuyến mại đã có trong database
                    var pro = await _promotionService.GetAllPromotionsAsync();
                    // Tìm khuyến mại phù hợp với điểm khách hàng
                    var applicablePromotion = pro
                        .Where(p => loyalPointOfCustomer.Points >= p.Points)
                        .OrderByDescending(p => p.Points)
                        .FirstOrDefault();
                    //
                    if (applicablePromotion == null)
                    {
                        throw new InvalidOperationException("Current promotions are currently unavailable for your situation");
                    }
                    //
                    // Tính toán giá trị mới cho TotalPrice dựa trên khuyến mại
                    var totalPrice = priceWithNoPromotion * applicablePromotion.Discount;

                    // Tạo hóa đơn mới
                    var newInvoice = new Invoice()
                    {
                        OrderId = orderId.Value,
                        PromotionId = applicablePromotion.PromotionId,
                        PromotionName = applicablePromotion.Name,
                        TotalPrice = totalPrice,
                        StaffId = staffId
                    };

                    // Lưu hóa đơn mới vào cơ sở dữ liệu
                    await _invoiceRepository.AddInvoiceAsync(newInvoice);
                }
            }


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
