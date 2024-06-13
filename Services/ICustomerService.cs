﻿using BackEnd.Models;
using BackEnd.ViewModels;


namespace BackEnd.Services
{
    public interface ICustomerService
    {
        Task<Customer?> RegisterCustomerAsync(Customer customer);
        //jwt string token
        Task<string> LoginAsync(LoginRequest loginRequest);

        Task<Customer?> GetCustomertByLastNameAsync(string LastName);

    }
}