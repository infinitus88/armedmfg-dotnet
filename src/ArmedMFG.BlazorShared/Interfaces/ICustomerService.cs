﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorShared.Interfaces;

public interface ICustomerService
{
    Task<Customer> Create(CreateCustomerRequest customer);
    Task<Customer> Edit(Customer customer);
    Task<string> Delete(int id);
    Task<Customer> GetById(int id);
    Task<List<Customer>> ListPaged(int pageSize);
    Task<List<AutocompleteCustomer>> ListAutocomplete(string fullName);
    Task<List<Customer>> List();
}
