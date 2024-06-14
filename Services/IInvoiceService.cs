using BackEnd.Models;

namespace BackEnd.Services
{
    public interface IInvoiceService
    {
        Task<Invoice?> GetInvoiceByIdAsync(int? oriderId);
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        Task AddInvoiceAsync(int? orderId,int? staffId);
        Task<Invoice> UpdateInvoiceAsync(Invoice invoice);
        Task<bool> DeleteInvoiceAsync(int invoiceId);
    }
}
