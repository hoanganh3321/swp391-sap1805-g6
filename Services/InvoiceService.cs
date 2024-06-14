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


        public InvoiceService(IInvoiceRepository invoiceRepository,
          IOrderRepository orderRepository,
          ICustomerRepository customerRepository)
        {
            _invoiceRepository = invoiceRepository;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(int? orderId)
        {
            return await _invoiceRepository.GetInvoiceByIdAsync(orderId);
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _invoiceRepository.GetAllInvoicesAsync();
        }
        public async Task AddInvoiceAsync(int? orderId)
        {
            if (orderId == null)
            {
                throw new ArgumentNullException(nameof(orderId));
            }

            // Lấy giá trị không đồng bộ của PriceWithNoPromotion
            var priceWithNoPromotion = await _orderRepository.GetPriceWithNoPromotionAsync(orderId);
            // Lấy giá trị không đồng bộ của customerId
            var customerId = await _orderRepository.SearchCustomerByOrderId(orderId);

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

            // Xác định promotion id, name và total price dựa trên loyalty points
            int promotionId;
            string promotionName;
            decimal? totalPrice;

            if (loyalPointOfCustomer.Points >= 100)
            {
                promotionId = 4;
                promotionName = "Loyal Point reach to 100 points";
                totalPrice = priceWithNoPromotion / 2;
            }
            else if (loyalPointOfCustomer.Points >= 50)
            {
                promotionId = 3;
                promotionName = "Loyal Point reach to 50 points";
                totalPrice = (priceWithNoPromotion * 3) / 10;
            }
            else if (loyalPointOfCustomer.Points >= 20)
            {
                promotionId = 2;
                promotionName = "Loyal Point reach to 20 points";
                totalPrice = (priceWithNoPromotion * 3) / 20;
            }
            else if (loyalPointOfCustomer.Points >= 10)
            {
                promotionId = 1;
                promotionName = "Loyal Point reach to 10 points";
                totalPrice = priceWithNoPromotion / 10;
            }
            else
            {
                throw new InvalidOperationException("Customer does not have enough loyalty points for any promotion.");
            }

            var invoice = new Invoice()
            {
                OrderId = orderId.Value,
                PromotionId = promotionId,
                PromotionName = promotionName,
                TotalPrice = totalPrice,
            };

            await _invoiceRepository.AddInvoiceAsync(invoice);
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
