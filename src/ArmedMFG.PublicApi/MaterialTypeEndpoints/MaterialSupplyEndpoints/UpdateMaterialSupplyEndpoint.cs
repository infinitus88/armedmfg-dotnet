using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class UpdateMaterialSupplyEndpoint : IEndpoint<IResult, UpdateMaterialSupplyRequest, IRepository<MaterialSupply>>
{
    private readonly IMapper _mapper;
    
    public UpdateMaterialSupplyEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/material-types/supplies",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (UpdateMaterialSupplyRequest request, IRepository<MaterialSupply> materialSupplyRepository) =>
                {
                    return await HandleAsync(request, materialSupplyRepository);
                })
            .Produces<UpdateMaterialSupplyResponse>()
            .WithTags("MaterialSupplyEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateMaterialSupplyRequest request, IRepository<MaterialSupply> materialSupplyRepository)
    {
        var response = new UpdateMaterialSupplyResponse(request.CorrelationId());

        var existingMaterialSupply = await materialSupplyRepository.GetByIdAsync(request.Id);

        MaterialSupply.MaterialSupplyDetails details = new(request.DeliveredDate, request.Price, request.Amount);
        existingMaterialSupply.UpdateDetails(details);

        await materialSupplyRepository.UpdateAsync(existingMaterialSupply);

        var dto = new MaterialSupplyDto
        {
            Id = existingMaterialSupply.Id,
            MaterialTypeId = existingMaterialSupply.MaterialTypeId,
            DeliveredDate = existingMaterialSupply.DeliveredDate,
            Price = existingMaterialSupply.Price,
            Amount = existingMaterialSupply.Amount
        };
        response.MaterialSupply = dto;
        return Results.Ok(response);
    }
}
