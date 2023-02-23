using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class UpdateMaterialTypeEndpoint : IEndpoint<IResult, UpdateMaterialTypeRequest, IRepository<MaterialType>>
{
    public UpdateMaterialTypeEndpoint()
    {
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/material-types",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (UpdateMaterialTypeRequest request, IRepository<MaterialType> materialTypeRepository) =>
                {
                    return await HandleAsync(request, materialTypeRepository);
                })
            .Produces<UpdateMaterialTypeResponse>()
            .WithTags("MaterialTypeEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateMaterialTypeRequest request, IRepository<MaterialType> materialTypeRepository)
    {
        var response = new UpdateMaterialTypeResponse(request.CorrelationId());

        var existingMaterialType = await materialTypeRepository.GetByIdAsync(request.Id);

        MaterialType.MaterialTypeDetails details = new(request.Name, request.Description);
        existingMaterialType.UpdateDetails(details);
        existingMaterialType.UpdateCategory(request.MaterialCategoryId);

        await materialTypeRepository.UpdateAsync(existingMaterialType);

        var dto = new MaterialTypeDto
        {
            Id = existingMaterialType.Id,
            MaterialCategoryId = existingMaterialType.MaterialCategoryId,
            Description = existingMaterialType.Description,
            Name = existingMaterialType.Name,
        };
        response.MaterialType = dto;
        return Results.Ok(response);
    }
}
