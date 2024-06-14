using BackEnd.Models;

namespace BackEnd.Reporitories
{
    public interface IReturnPolicyRepository
    {
        Task<ReturnPolicy> AddReturnPolicyAsync(ReturnPolicy returnPolicy);
        Task<ReturnPolicy?> GetReturnPolicybyId(int? id);
        Task<IEnumerable<ReturnPolicy>> GetAllReturnPoliciesAsync();
        Task<bool> DeleteReturnPolicyAsync(int id);
        Task<bool> UpdateReturnPolicyAsync(int id, ReturnPolicy returnPolicy);

    }
}
