using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class GetByIdMaterialSupplyEndpoint : IEndpoint<IResult, GetByIdMaterialSupplyRequest, IRepository<MaterialSupply>>
{
    private readonly IUriComposer _uriComposer;

    public GetByIdMaterialSupplyEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/material-types/supplies/{materialSupplyId}",
                async (int materialSupplyId, IRepository<MaterialSupply> materialSupplyRepository) =>
                {
                    return await HandleAsync(new GetByIdMaterialSupplyRequest(materialSupplyId), materialSupplyRepository);
                })
            .Produces<GetByIdMaterialSupplyResponse>()
            .WithTags("MaterialSupplyEndpoints");
    }
    
    public async Task<IResult> HandleAsync(GetByIdMaterialSupplyRequest request, IRepository<MaterialSupply> materialSupplyRepository)
    {
        var response = new GetByIdMaterialSupplyResponse(request.CorrelationId());

        var materialSupply = await materialSupplyRepository.GetByIdAsync(request.MaterialSupplyId);
        if (materialSupply is null)
            return Results.NotFound();

        response.MaterialSupply = new MaterialSupplyDto
        {
            Id = materialSupply.Id,
            DeliveredDate = materialSupply.DeliveredDate,
            MaterialTypeId = materialSupply.MaterialTypeId,
            UnitPrice = materialSupply.Price,
            Amount = materialSupply.Amount
        };

        return Results.Ok(response);
    }
}
