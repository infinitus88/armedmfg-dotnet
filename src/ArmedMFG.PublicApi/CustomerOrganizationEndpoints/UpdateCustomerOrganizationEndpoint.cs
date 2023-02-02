using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using ArmedMFG.ApplicationCore.Exceptions;
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

        if (existingOrganization is null)
        {
            throw new NotFoundException($"The customer organization with Id : {request.Id} was not found");
        }
        existingOrganization.SetAddress(request.MainBranchAddress.Region, request.MainBranchAddress.District, request.MainBranchAddress.Street);

        CustomerOrganization.OrganizationDetails details = new(request.Name, request.TaxpayerIdNum, request.PhoneNumber, request.Email, request.Description);
        existingOrganization.UpdateDetails(details);

        await organizationRepository.UpdateAsync(existingOrganization);

        var dto = new CustomerOrganizationDto()
        {
            Id = existingOrganization.Id,
            Name = existingOrganization.Name,
            TaxpayerIdNum = existingOrganization.TaxpayerIdNum,
            PhoneNumber = existingOrganization.PhoneNumber,
            Email = existingOrganization.Email,
            MainBranchAddress = existingOrganization.MainBranchAddress.ToString(),
            Description = existingOrganization.Description
        };
        response.Organization = dto;
        return Results.Ok(response);
    }
}
