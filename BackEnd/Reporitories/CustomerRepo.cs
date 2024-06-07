
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using swp391_sap1805_g6.Entities;

namespace swp391_sap1805_g6.Reporitories
{
    public class CustomerRepo
    {
        private static BanhangContext? _context;

        public static bool CusTExist(int id)
        {
            _context = new();
            return _context.Customers.Any(c => c.CustomerId == id);
        }
        public static List<Customer>? GetAllCustomers()
        {
            _context = new();
            return _context.Customers.Include(c=>c.Orders).ToList();                      
        }

        public static Customer? GetCusById(int id)
        {
            _context = new();
            return _context.Customers.FirstOrDefault(x => x.CustomerId == id);
        }

        public static void Add(Customer customer)
        {

            _context = new();
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
    }
}
