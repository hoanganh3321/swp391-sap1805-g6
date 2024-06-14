using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BackEnd;
using BackEnd.Models;
using BackEnd.Reporitories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class CustomerRepository : ICustomerRepository
{
    private readonly Banhang3Context _context;

    public CustomerRepository(Banhang3Context context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Customer?> AddCustomerAsync(Customer customer)
    {

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }


    public async Task<IEnumerable<Customer>> GetCustomersByLastNameAsync(string lastName)
    {
        return await _context.Customers
                             .Where(c => c.LastName.Contains(lastName))
                             .ToListAsync();
    }
    public async Task<Customer?> GetCustomerByLastName2Async(string lastName)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.LastName == lastName);
    }
    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));

        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task DeleteCustomerAsync(int customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Customer?> GetCustomerByAddressAsync(string Address)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Address == Address);
    }

    public async Task<Customer?> GetCustomerByPhoneNumberAsync(string PhoneNumber)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == PhoneNumber);
    }
    public async Task<LoyaltyPoint?> GetCustomerLoyalPointByCustomerId1(int? customerId)
    {
        var customer = await _context.Customers
       .Include(c => c.LoyaltyPoints)
       .FirstOrDefaultAsync(c => c.CustomerId == customerId);

        if (customer == null || !customer.LoyaltyPoints.Any())
        {
            return null;
        }
        return customer.LoyaltyPoints.FirstOrDefault();
    }
}

