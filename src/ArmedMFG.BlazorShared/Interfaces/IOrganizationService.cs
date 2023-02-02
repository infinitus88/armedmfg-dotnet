using System.Collections.Generic;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.BlazorShared.Interfaces;

public interface IOrganizationService
{
    Task<Organization> Create(CreateOrganizationRequest organization);
    Task<Organization> Edit(Organization organization);
    Task<string> Delete(int id);
    Task<Organization> GetById(int id);
    Task<List<Organization>> ListPaged(int pageSize);
    Task<List<Organization>> List();
}
