using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using ArmedMFG.PublicApi.Configuration;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ArmedMFG.PublicApi.Modules.Customers.Dtos;
using ArmedMFG.PublicApi.Modules.Customers.Dtos.Shared;
using ArmedMFG.ApplicationCore.Entities.CustomerAggregate;
using ArmedMFG.ApplicationCore.Specifications.Customers;

namespace ArmedMFG.PublicApi.Modules.Customers;
[Route("api/customers")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly DateParsingSettings _dateParsingSettings;
    private readonly IRepository<Customer> _customerRepository;

    public CustomersController(IMapper mapper, DateParsingSettings dateParsingSettings, IRepository<Customer> customerRepository)
    {
        _mapper = mapper;
        _dateParsingSettings = dateParsingSettings;
        _customerRepository = customerRepository;
    }

    [HttpPost]
    [Obsolete]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Create([FromBody] CreateCustomerRequest request)
    {
        var response = new CreateCustomerResponse(request.CorrelationId());

        var newCustomer = new Customer(request.FullName, 
            request.PhoneNumber,
            (CustomerPosition)request.Position,
            request.Email,
            request.IsBusiness,
            request.TaxId,
            request.OrganizationName,
            (FindOutThrough)request.FindOutThrough,
            request.Description);

        newCustomer = await _customerRepository.AddAsync(newCustomer);

        response.Customer = _mapper.Map<CustomerDto>(newCustomer);
        return Results.Created($"api/customers/{newCustomer.Id}", response);
    }

    [HttpGet("options")]
    public async Task<IResult> GetCustomerOptions()
    {
        var response = new GetCustomerOptionsResponse();

        var customers = await _customerRepository.ListAsync();

        response.Customers.AddRange(customers.Select(_mapper.Map<CustomerOptionDto>));

        return Results.Ok(response);
    }

    [HttpGet("{customerId}")]
    public async Task<IResult> GetPaymentRecordForEdit(int customerId)
    {
        var response = new GetCustomerForEditResponse();

        var customerForEdit = await _customerRepository.GetByIdAsync(customerId);
        if (customerForEdit is null)
            return Results.NotFound();

        response.Customer = _mapper.Map<CustomerForEditDto>(customerForEdit);

        return Results.Ok(response);
    }

    [HttpPost("search")]
    public async Task<IResult> Search([FromBody] SearchCustomerRequest request)
    {
        var response = new SearchCustomerResponse(request.CorrelationId());

        var filterSpec = new SearchCustomerFilterSpecification(request.Filter.SearchText);
        int totalItems = await _customerRepository.CountAsync(filterSpec);

        var pagedSpec = new SearchCustomerFilterPaginatedSpecification(
            skip: (request.PageNumber.Value - 1) * request.PageSize.Value,
            take: request.PageSize.Value,
            request.Filter.SearchText);

        var customers = await _customerRepository.ListAsync(pagedSpec);

        response.Customers.AddRange(customers.Select(_mapper.Map<CustomerDto>));
        response.TotalCount = totalItems;

        return Results.Ok(response);
    }

    [HttpPut]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Update([FromBody] UpdateCustomerRequest request)
    {
        var response = new UpdateCustomerResponse(request.CorrelationId());

        var existingCustomer = await _customerRepository.GetByIdAsync(request.Id);

        if (existingCustomer is null)
            return Results.NotFound();

        Customer.CustomerDetails details = new(
            request.FullName,
            request.PhoneNumber,
            request.Email,
            (CustomerPosition)request.Position,
            request.IsBusiness,
            request.TaxId,
            request.OrganizationName,
            (FindOutThrough)request.FindOutThrough,
            request.Description);
        existingCustomer.UpdateDetails(details);

        await _customerRepository.UpdateAsync(existingCustomer);

        response.Customer = _mapper.Map<CustomerDto>(existingCustomer);

        return Results.Ok(response);
    }

    [HttpDelete("{customerId}")]
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> SingleDelete(int customerId)
    {
        var response = new DeleteSingleCustomerResponse();

        var customerToDelete = await _customerRepository.GetByIdAsync(customerId);
        if (customerToDelete is null)
            return Results.NotFound();

        await _customerRepository.DeleteAsync(customerToDelete);

        return Results.Ok(response);
    }
}
