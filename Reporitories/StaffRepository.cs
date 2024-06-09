using BackEnd.Models;
using BackEnd.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Reporitories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly Banhang3Context _context;

        public StaffRepository(Banhang3Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Staff?> GetAdminByEmailAsync(string email)
        {
            return await _context.Staff.FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
