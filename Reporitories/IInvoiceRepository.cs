using BackEnd.Models;

namespace BackEnd.Reporitories
{
    public interface IInvoiceRepository
    {
        Task<Invoice?> GetInvoiceByIdAsync(int? orderId);
        Task<List<Invoice>> GetAllInvoicesAsync();
        Task<Invoice> AddInvoiceAsync(Invoice invoice);
        Task<Invoice> UpdateInvoiceAsync(Invoice invoice);
        Task<bool> DeleteInvoiceAsync(int invoiceId);
        Task<List<Invoice>> GetInVoiceAsync(int? staffId);
        Task<Invoice?> GetOldInvoiceByOrderAsync(int? orderId);
    }
}
