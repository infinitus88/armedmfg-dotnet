using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.MaterialTypeEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class GetByIdMaterialTypeEndpoint : IEndpoint<IResult, GetByIdMaterialTypeRequest, IRepository<MaterialType>>
{
    private readonly IUriComposer _uriComposer;

    public GetByIdMaterialTypeEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/material-type/{materialTypeId}",
                async (int materialTypeId, IRepository<MaterialType> materialTypeRepository) =>
                {
                    return await HandleAsync(new GetByIdMaterialTypeRequest(materialTypeId), materialTypeRepository);
                })
            .Produces<GetByIdMaterialTypeResponse>()
            .WithTags("MaterialTypeEndpoints");
    }
    
    public async Task<IResult> HandleAsync(GetByIdMaterialTypeRequest request, IRepository<MaterialType> materialTypeRepository)
    {
        var response = new GetByIdMaterialTypeResponse(request.CorrelationId());

        var materialType = await materialTypeRepository.GetByIdAsync(request.MaterialTypeId);
        if (materialType is null)
            return Results.NotFound();

        response.MaterialType = new MaterialTypeDto
        {
            Id = materialType.Id,
            MaterialCategoryId = materialType.MaterialCategoryId,
            Description = materialType.Description,
            Name = materialType.Name,
        };

        return Results.Ok(response);
    }
}
