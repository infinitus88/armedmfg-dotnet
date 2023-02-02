﻿using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Exceptions;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class CreateCustomerEndpoint : IEndpoint<IResult, CreateCustomerRequest, IRepository<Customer>, IRepository<CustomerOrganization>>
{
    private readonly IMapper _mapper;

    public CreateCustomerEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/customers",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS,
                    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                async
                    (CreateCustomerRequest request, IRepository<Customer> customerRepository, IRepository<CustomerOrganization> organizationRepository) =>
                {
                    return await HandleAsync(request, customerRepository, organizationRepository);
                })
            .Produces<CreateCustomerResponse>()
            .WithTags("CustomerEndpoints");
    }
    
    public async Task<IResult> HandleAsync(CreateCustomerRequest request,
        IRepository<Customer> customerRepository, IRepository<CustomerOrganization> organizationRepository)
    {
        var response = new CreateCustomerResponse(request.CorrelationId());
        
        // var productPriceNameSpecification = new ProductPrice
        

        var newCustomer = new Customer(request.FullName, request.PhoneNumber, request.FindOutThrough);
        newCustomer = await customerRepository.AddAsync(newCustomer);
        
        if (request.OrganizationId.HasValue)
        {
            var existingOrganization = await organizationRepository.GetByIdAsync(request.OrganizationId);
            
            if (existingOrganization == null)
            {
                throw new NotFoundException($"A customer organization with Id: {request.OrganizationId} not be found");
            } 
            
            newCustomer.SetOrganization(existingOrganization.Id);
        }

        var dto = new CustomerDto
        {
            Id = newCustomer.Id,
            FullName = newCustomer.FullName,
            PhoneNumber = newCustomer.PhoneNumber,
            Email = newCustomer.Email,
            OrganizationId = newCustomer.OrganizationId
        };
        response.Customer = dto;
        return Results.Created($"api/customers/{dto.Id}", response);
    }
}
