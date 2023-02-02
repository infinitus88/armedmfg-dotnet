using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.CustomerEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class UpdateCustomerOrganizationEndpoint : IEndpoint<IResult, UpdateCustomerOrganizationRequest, IRepository<CustomerOrganization>>
{
    private readonly IMapper _mapper;
    
    public UpdateCustomerOrganizationEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/customers/organizations",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (UpdateCustomerOrganizationRequest request, IRepository<CustomerOrganization> organizationRepository) =>
                {
                    return await HandleAsync(request, organizationRepository);
                })
            .Produces<UpdateCustomerOrganizationResponse>()
            .WithTags("CustomerOrganizationEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateCustomerOrganizationRequest request, IRepository<CustomerOrganization> organizationRepository)
    {
        var response = new UpdateCustomerOrganizationResponse(request.CorrelationId());

        var existingOrganization = await organizationRepository.GetByIdAsync(request.Id);

        var address = new Address(request.MainBranchAddress.Region, request.MainBranchAddress.District,
            request.MainBranchAddress.Street);

        CustomerOrganization.OrganizationDetails details = new(request.Name, request.PhoneNumber, request.Email, address, request.Description);
        existingOrganization.UpdateDetails(details);

        await organizationRepository.UpdateAsync(existingOrganization);

        var dto = new CustomerOrganizationDto()
        {
            Id = existingOrganization.Id,
            Name = existingOrganization.Name,
            PhoneNumber = existingOrganization.PhoneNumber,
            MainBranchAddress = existingOrganization.MainBranchAddress.ToString(),
            Email = existingOrganization.Email,
        };
        response.Organization = dto;
        return Results.Ok(response);
    }
}
