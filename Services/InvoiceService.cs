using BackEnd.Models;
using BackEnd.Reporitories;

namespace BackEnd.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(int? invoiceId)
        {
            return await _invoiceRepository.GetInvoiceByIdAsync(invoiceId);
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _invoiceRepository.GetAllInvoicesAsync();
        }

        public async Task<Invoice> AddInvoiceAsync(Invoice invoice)
        {
            return await _invoiceRepository.AddInvoiceAsync(invoice);
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
