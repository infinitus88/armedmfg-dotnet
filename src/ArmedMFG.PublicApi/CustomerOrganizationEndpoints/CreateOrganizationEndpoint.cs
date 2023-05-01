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

public class CreateOrganizationEndpoint : IEndpoint<IResult, CreateOrganizationRequest, IRepository<CustomerOrganization>>
{
    private readonly IMapper _mapper;

    public CreateOrganizationEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/customers/organizations",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS,
                    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                async
                    (CreateOrganizationRequest request, IRepository<CustomerOrganization> customerOrganizationRepository) =>
                {
                    return await HandleAsync(request, customerOrganizationRepository);
                })
            .Produces<CreateOrganizationResponse>()
            .WithTags("CustomerOrganizationEndpoints");
    }
    
    public async Task<IResult> HandleAsync(CreateOrganizationRequest request,
        IRepository<CustomerOrganization> customerOrganizationRepository)
    {
        var response = new CreateOrganizationResponse(request.CorrelationId());
        
        // var productPriceNameSpecification = new ProductPrice
        
        var newOrganization = new CustomerOrganization(request.Name, request.TaxpayerIdNum, request.PhoneNumber, request.Email, request.Description);
        newOrganization.SetAddress(request.Region, request.District, request.Street);
        
        newOrganization = await customerOrganizationRepository.AddAsync(newOrganization);
        
        response.Organization = _mapper.Map<OrganizationInfoDto>(newOrganization);
        
        return Results.Created($"api/customers/organizations/{newOrganization.Id}", response);
    }
}
