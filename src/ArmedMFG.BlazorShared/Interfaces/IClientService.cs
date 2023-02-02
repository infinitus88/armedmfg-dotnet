using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorShared.Interfaces;

public interface IClientService
{
    Task<Customer> Create(CreateCustomerRequest customer);
    Task<Customer> Edit(Customer customer);
    Task<string> Delete(int id);
    Task<Customer> GetById(int id);
    Task<List<Customer>> ListPaged(int pageSize);
    Task<List<Customer>> List();
}
