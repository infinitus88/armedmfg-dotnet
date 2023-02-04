using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorShared.Interfaces;

public interface ICustomerOrganizationService
{
    Task<CustomerOrganization> Create(CreateCustomerOrganizationRequest customerOrganization);
    Task<CustomerOrganization> Edit(CustomerOrganization customerOrganization);
    Task<string> Delete(int id);
    Task<CustomerOrganization> GetById(int id);
    Task<List<CustomerOrganization>> ListPaged(int pageSize);
    Task<List<CustomerOrganization>> List();
}
