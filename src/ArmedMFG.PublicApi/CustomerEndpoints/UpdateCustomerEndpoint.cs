using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.CustomerEndpoints;

public class UpdateCustomerEndpoint : IEndpoint<IResult, UpdateCustomerRequest, IRepository<Customer>>
{
    private readonly IMapper _mapper;
    
    public UpdateCustomerEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/customers",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (UpdateCustomerRequest request, IRepository<Customer> customerRepository) =>
                {
                    return await HandleAsync(request, customerRepository);
                })
            .Produces<UpdateCustomerResponse>()
            .WithTags("CustomerEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateCustomerRequest request, IRepository<Customer> customerRepository)
    {
        var response = new UpdateCustomerResponse(request.CorrelationId());

        var existingCustomer = await customerRepository.GetByIdAsync(request.Id);

        Customer.CustomerDetails details = new(request.FullName, request.PhoneNumber, request.Email, request.FindOutThrough);
        existingCustomer.UpdateDetails(details);

        await customerRepository.UpdateAsync(existingCustomer);

        var dto = new CustomerDto
        {
            Id = existingCustomer.Id,
            FullName = existingCustomer.FullName,
            PhoneNumber = existingCustomer.PhoneNumber,
            Email = existingCustomer.Email,
            OrganizationId = existingCustomer.OrganizationId
        };
        response.Customer = dto;
        return Results.Ok(response);
    }
}
