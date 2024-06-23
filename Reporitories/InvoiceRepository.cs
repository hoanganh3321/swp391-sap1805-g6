using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BackEnd.Reporitories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly Banhang3Context _context;

        public InvoiceRepository(Banhang3Context context)
        {
            _context = context;
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(int? orderId)
        {
            if (orderId == null)
            {
                throw new ArgumentNullException(nameof(orderId));
            }

            return await _context.Invoices
                                 .Include(i => i.Order) // Bao gồm bảng Order
                                 .ThenInclude(o=>o.OrderDetails) // Bao gồm bảng OrderDetails
                                 .FirstOrDefaultAsync(i => i.OrderId == orderId);
        }

        public async Task<Invoice?> GetOldInvoiceByOrderAsync(int? orderId)
        {
            if (orderId == null)
            {
                throw new ArgumentNullException(nameof(orderId));
            }

            return await _context.Invoices                                
                                 .FirstOrDefaultAsync(i => i.OrderId == orderId);
        }
        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            return await _context.Invoices.ToListAsync();
        }

        public async Task<Invoice> AddInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task<Invoice> UpdateInvoiceAsync(Invoice invoice)
        {
            _context.Entry(invoice).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task<bool> DeleteInvoiceAsync(int invoiceId)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice == null)
            {
                return false;
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Invoice>> GetInVoiceAsync(int? staffId)
        {
            return await _context.Invoices.Where(i => i.StaffId == staffId).ToListAsync();
        }
    }
}
