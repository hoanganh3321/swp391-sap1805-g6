using BackEnd.Models;

namespace BackEnd.Reporitories
{
    public interface ILoyalPointRepository
    {
        Task<LoyaltyPoint> AddLoyalPointAsync(LoyaltyPoint LoyalPointRepository);
        Task<IEnumerable<LoyaltyPoint>> GetAllLoyalPointsAsync();
        Task<bool> UpdateLoyalPointAsync(int? customerId, LoyaltyPoint LoyalPoint);
        Task<bool> DeletePointAsync(int ID);
        Task<LoyaltyPoint?> GetByIdAsync(int id);

        Task<LoyaltyPoint?> GetLoyalty(int customerId);
        Task<int?> GetLoyalIdByCustomerIdAsync(int customerId);
        Task<int?> GetPoints(int? customerId);
    }
}
