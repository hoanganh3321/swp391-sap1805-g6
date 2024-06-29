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

        public async Task<IEnumerable<Staff>> GetAllStaff()
        {
            return await _context.Staff.ToListAsync();
        }

        public async Task<Staff?> GetStaffByEmailAsync(string email)
        {
            return await _context.Staff.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Store> GetStoreByStaffIdAsync(int? staffId)
        {
            var staff = await _context.Staff
                .Include(s => s.Store) // Đảm bảo Staff được load với thông tin của Store
                .FirstOrDefaultAsync(s => s.StaffId == staffId);

            if (staff == null)
            {
                throw new ArgumentException($"Staff with ID {staffId} not found.");
            }

            return staff.Store;
        }
    }
}
