using BackEnd.Models;


namespace BackEnd.Reporitories
{
    public interface ICustomerRepository
    {
        Task<Customer?> AddCustomerAsync(Customer customer);
         
        Task<Customer?> GetCustomerByEmailAsync(string email);
        Task<IEnumerable<Customer>> GetCustomersByLastNameAsync(string lastName);

        Task<Customer?> GetCustomerByLastName2Async(string lastName);

        Task<Customer?> GetCustomerByAddressAsync(string Address);
        Task<Customer?> GetCustomerByPhoneNumberAsync(string PhoneNumber);
        Task<Customer> UpdateCustomerAsync(Customer customer);//
        Task<IEnumerable<Customer>> GetAllAsync();

        Task DeleteCustomerAsync(int customerId);//
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task<LoyaltyPoint?> GetCustomerLoyalPointByCustomerId1(int? customerId);
    }

}
