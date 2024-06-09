using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Reporitories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly Banhang3Context _context;

        public AdminRepository(Banhang3Context context)
        {
            _context = context;
        }

        public async Task<Admin?> GetAdminByEmailAsync(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
