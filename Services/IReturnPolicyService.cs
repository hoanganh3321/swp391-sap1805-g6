using BackEnd.Models;

namespace BackEnd.Services
{
    public interface IReturnPolicyService
    {
        Task<ReturnPolicy> AddReturnPolicyAsync(ReturnPolicy returnPolicy);
        Task<ReturnPolicy?> SearchReturnPolicyAsync(int? policyId);
        Task<IEnumerable<ReturnPolicy>> GetAllReturnPoliciesAsync();
        Task<bool> UpdateReturnPolicyAsync(int id, ReturnPolicy returnPolicy);
        Task<bool> DeleteReturnPolicyAsync(int id);
    }
}
