using BackEnd.Models;

namespace BackEnd.Services
{
    public interface IInvoiceService
    {
        Task<Invoice?> GetInvoiceByIdAsync(int? invoiceId);
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        Task<Invoice> AddInvoiceAsync(Invoice invoice);
        Task<Invoice> UpdateInvoiceAsync(Invoice invoice);
        Task<bool> DeleteInvoiceAsync(int invoiceId);
    }
}
