using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class CustomerOrganizationService : ICustomerOrganizationService
{
    private readonly HttpService _httpService;
    private readonly ILogger<CustomerOrganizationService> _logger;

    public CustomerOrganizationService(HttpService httpService, ILogger<CustomerOrganizationService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    } 
    
    public async Task<CustomerOrganization> Create(CreateCustomerOrganizationRequest organization)
    {
        var response = await _httpService.HttpPost<CreateCustomerOrganizationResponse>("customers/organizations", organization);
        return response?.Organization;
    }

    public async Task<CustomerOrganization> Edit(CustomerOrganization organization)
    {
        return (await _httpService.HttpPut<EditCustomerOrganizationResult>("customers/organization", organization)).Organization;
    }

    public async Task<string> Delete(int id)
    {
        return (await _httpService.HttpDelete<DeleteCustomerOrganizationResponse>("customers/organizations", id)).Status;
    }

    public async Task<CustomerOrganization> GetById(int id)
    {
        var organizationGetTask = await _httpService.HttpGet<EditCustomerOrganizationResult>($"customers/organizations/{id}");
        
        var organization = organizationGetTask.Organization;
        
        return organization;
    }

    public async Task<List<CustomerOrganization>> ListPaged(int pageSize)
    {
        _logger.LogInformation("Fetching organizations from API.");

        var organizationListTask = await _httpService.HttpGet<PagedCustomerOrganizationResponse>($"customers/organizations?PageSize={pageSize}");
        
        var organizations = organizationListTask.Organizations;

        return organizations;
    }

    public async Task<List<CustomerOrganization>> List()
    {
        _logger.LogInformation("Fetching organizations from API.");

        var organizationListTask = await _httpService.HttpGet<PagedCustomerOrganizationResponse>($"customers/organizations");
        
        var organizations = organizationListTask.Organizations;

        return organizations;
    } 
}
