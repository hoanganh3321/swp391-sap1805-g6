using BackEnd.Models;
using BackEnd.Reporitories;

namespace BackEnd.Services
{
    public class ReturnPolicyService : IReturnPolicyService
    {
        private readonly IReturnPolicyRepository _returnPolicyRepository;

        public ReturnPolicyService(IReturnPolicyRepository returnPolicyRepository)
        {
            _returnPolicyRepository = returnPolicyRepository;
        }

        public async Task<ReturnPolicy> AddReturnPolicyAsync(ReturnPolicy returnPolicy)
        {
            return await _returnPolicyRepository.AddReturnPolicyAsync(returnPolicy);
        }
        public async Task<ReturnPolicy?> SearchReturnPolicyAsync(int? policyId)
        {
            return await _returnPolicyRepository.GetReturnPolicybyId(policyId);
        }
        public async Task<bool> DeleteReturnPolicyAsync(int id)
        {
            return await _returnPolicyRepository.DeleteReturnPolicyAsync(id);
        }

        public async Task<IEnumerable<ReturnPolicy>> GetAllReturnPoliciesAsync()
        {
            return await _returnPolicyRepository.GetAllReturnPoliciesAsync();
        }

        public async Task<bool> UpdateReturnPolicyAsync(int id, ReturnPolicy returnPolicy)
        {
            // Validate the policy description before updating
            if (!ValidatePolicyDescription(returnPolicy))
            {
                throw new ArgumentException("Description is not valid.");
            }

            return await _returnPolicyRepository.UpdateReturnPolicyAsync(id, returnPolicy);
        }
        private bool ValidatePolicyDescription(ReturnPolicy returnPolicy)
        {
            return !string.IsNullOrWhiteSpace(returnPolicy.Description);
        }
    }
}
