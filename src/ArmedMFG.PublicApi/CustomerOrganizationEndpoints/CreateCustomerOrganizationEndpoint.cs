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

namespace ArmedMFG.PublicApi.CustomerOrganizationEndpoints;

public class CreateCustomerOrganizationEndpoint : IEndpoint<IResult, CreateCustomerOrganizationRequest, IRepository<CustomerOrganization>>
{
    private readonly IMapper _mapper;

    public CreateCustomerOrganizationEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/customers/organizations",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS,
                    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                async
                    (CreateCustomerOrganizationRequest request, IRepository<CustomerOrganization> customerOrganizationRepository) =>
                {
                    return await HandleAsync(request, customerOrganizationRepository);
                })
            .Produces<CreateCustomerOrganizationResponse>()
            .WithTags("CustomerOrganizationEndpoints");
    }
    
    public async Task<IResult> HandleAsync(CreateCustomerOrganizationRequest request,
        IRepository<CustomerOrganization> customerOrganizationRepository)
    {
        var response = new CreateCustomerOrganizationResponse(request.CorrelationId());
        
        // var productPriceNameSpecification = new ProductPrice
        
        var newOrganization = new CustomerOrganization(request.Name, request.TaxpayerIdNum, request.PhoneNumber, request.Email, request.Description);
        newOrganization.SetAddress(request.MainBranchAddress.Region, request.MainBranchAddress.District, request.MainBranchAddress.Street);
        
        newOrganization = await customerOrganizationRepository.AddAsync(newOrganization);
        
        var dto = new CustomerOrganizationDto
        {
            Id = newOrganization.Id,
            Name = newOrganization.Name,
            TaxpayerIdNum = newOrganization.TaxpayerIdNum,
            PhoneNumber = newOrganization.PhoneNumber,
            Email = newOrganization.Email,
            MainBranchAddress = newOrganization.MainBranchAddress.ToString(),
            Description = newOrganization.Description
        };

        response.Organization = dto;
        
        return Results.Created($"api/customers/organizations/{dto.Id}", response);
    }
}
