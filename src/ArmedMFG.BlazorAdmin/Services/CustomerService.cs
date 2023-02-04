using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.BlazorShared.Interfaces;
using ArmedMFG.BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace ArmedMFG.BlazorAdmin.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerOrganizationService _organizationService;
    private readonly HttpService _httpService;
    private readonly ILogger<Customer> _logger;

    public CustomerService(HttpService httpService, ILogger<Customer> logger, ICustomerOrganizationService organizationService)
    {
        _httpService = httpService;
        _logger = logger;
        _organizationService = organizationService;
    } 
    
    public async Task<Customer> Create(CreateCustomerRequest customer)
    {
        var response = await _httpService.HttpPost<CreateCustomerResponse>("customers", customer);
        return response?.Customer;
    }

    public async Task<Customer> Edit(Customer customer)
    {
        return (await _httpService.HttpPut<EditCustomerResult>("customers", customer)).Customer;
    }

    public async Task<string> Delete(int id)
    {
        return (await _httpService.HttpDelete<DeleteCustomerResponse>("customers", id)).Status;
    }

    public async Task<Customer> GetById(int id)
    {
        var organizationListTask = _organizationService.List();
        var customerGetTask = _httpService.HttpGet<EditCustomerResult>($"customers/{id}");
        await Task.WhenAll(organizationListTask,customerGetTask);
        
        var organizations = organizationListTask.Result;
        var customer = customerGetTask.Result.Customer;

        if (customer.OrganizationId == 0)
        {
            customer.Organization = organizations.FirstOrDefault(o => o.Id == customer.OrganizationId)?.Name;
        }
        
        return customer;
    }

    public async Task<List<Customer>> ListPaged(int pageSize)
    {
        _logger.LogInformation("Fetching customers from API.");

        var organizationListTask = _organizationService.List();
        var customersListTask = _httpService.HttpGet<PagedCustomerResponse>($"customers?PageSize={pageSize}");
        await Task.WhenAll(organizationListTask, customersListTask);
        
        var organizations = organizationListTask.Result;
        var customers = customersListTask.Result.Customers;

        foreach (var customer in customers)
        {
            customer.Organization = customer.OrganizationId == 0 ? "Not Set" : organizations.FirstOrDefault(o => o.Id == customer.OrganizationId)?.Name;
        }

        return customers;
    }

    public async Task<List<Customer>> List()
    {
        _logger.LogInformation("Fetching customers from API.");

        var organizationListTask = _organizationService.List();
        var customerListTask = _httpService.HttpGet<PagedCustomerResponse>($"customers");
        await Task.WhenAll(organizationListTask, customerListTask);
        
        var organizations = organizationListTask.Result;
        var customers = customerListTask.Result.Customers;

        foreach (var customer in customers)
        {
            customer.Organization = customer.OrganizationId == 0 ? "Not Set" : organizations.FirstOrDefault(o => o.Id == customer.OrganizationId)?.Name;
        }

        return customers;
    }
}
