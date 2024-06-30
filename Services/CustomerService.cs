using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackEnd.Models;
using BackEnd.Reporitories;
using BackEnd.ViewModels;
using Microsoft.IdentityModel.Tokens;


namespace BackEnd.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _config;
        
        

        public CustomerService(ICustomerRepository customerRepository,IConfiguration congig)
        {
            _customerRepository = customerRepository;
            _config = congig;
        }

        public async Task<string> LoginAsync(LoginRequest loginRequest)
        {
        
            var customer = await _customerRepository.GetCustomerByEmailAsync(loginRequest.Email);

            if (customer == null)
            {
                return "Invalid email.";
            }
            

            if (customer.Password != loginRequest.Password)
            {
                return "Invalid password.";
            }
            
            
                var token = GenerateJwtToken(customer);
                return token;
            
            
        }
        private string GenerateJwtToken(Customer customer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"] ?? "");          
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, customer.CustomerId.ToString()),
                new Claim(ClaimTypes.Email, customer.Email)
            }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenHandler.WriteToken(token);
        }


        public async Task<Customer?> RegisterCustomerAsync(Customer customer)
        {
        
            // Check if the email is already used by another customer
            var existingCustomerByEmail = await _customerRepository.GetCustomerByEmailAsync(customer.Email);
            if (existingCustomerByEmail != null)
            {
                throw new InvalidOperationException("Email is already used by another customer.");
            }

            // Check if the last name is already used by another customer
        
            var existingCustomerByLastName = await _customerRepository.GetCustomerByLastName2Async(customer.LastName ?? "");
            if (existingCustomerByLastName != null)
            {
                throw new InvalidOperationException("Last name is already used by another customer.");
            }
            // Check if the address is already used by another customer
            var existingCustomerByAddress = await _customerRepository.GetCustomerByAddressAsync(customer.Address ?? "");
            if (existingCustomerByAddress != null)
            {
                throw new InvalidOperationException("Address is already used by another customer.");
            }
            // Check if the phone is already used by another customer
            var existingCustomerByPhone = await _customerRepository.GetCustomerByPhoneNumberAsync(customer.PhoneNumber ?? "");
            if (existingCustomerByPhone != null)
            {
                throw new InvalidOperationException("Phone Number is already used by another customer.");
            }


            // Add the new customer
            return await _customerRepository.AddCustomerAsync(customer);
        }


        public async Task<Customer?> GetCustomertByLastNameAsync(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                return null;
            }

            var existingCustomerByLastName = await _customerRepository.GetCustomersByLastNameAsync(lastName);
            if (existingCustomerByLastName == null) { throw new Exception("customer not found"); }
            return existingCustomerByLastName.FirstOrDefault();
        }
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }
        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
        }

    }
}
