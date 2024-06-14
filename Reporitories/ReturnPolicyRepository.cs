using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Reporitories
{
    public class ReturnPolicyRepository : IReturnPolicyRepository
    {
        private readonly Banhang3Context _context;

        public ReturnPolicyRepository(Banhang3Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ReturnPolicy> AddReturnPolicyAsync(ReturnPolicy returnPolicy)
        {
            _context.ReturnPolicy.Add(returnPolicy);
            await _context.SaveChangesAsync();
            return returnPolicy;
        }

        public async Task<ReturnPolicy?> GetReturnPolicybyId(int? id)
        {
            return await _context.ReturnPolicy.FindAsync(id);
        }

        public async Task<IEnumerable<ReturnPolicy>> GetAllReturnPoliciesAsync()
        {
            return await _context.ReturnPolicy.ToListAsync();
        }

        public async Task<bool> DeleteReturnPolicyAsync(int id)
        {
            var returnPolicy = await _context.ReturnPolicy.FindAsync(id);
            if (returnPolicy == null)
            {
                return false;
            }

            _context.ReturnPolicy.Remove(returnPolicy);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateReturnPolicyAsync(int id, ReturnPolicy returnPolicy)
        {
            var existingReturnPolicy = await _context.ReturnPolicy.FindAsync(id);
            if (existingReturnPolicy == null)
            {
                return false;
            }

            existingReturnPolicy.Description = returnPolicy.Description;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
