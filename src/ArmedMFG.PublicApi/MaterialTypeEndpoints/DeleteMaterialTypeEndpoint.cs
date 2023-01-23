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

public class DeleteMaterialTypeEndpoint : IEndpoint<IResult, DeleteMaterialTypeRequest, IRepository<MaterialType>>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/material-types/{materialTypeId}",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (int materialTypeId, IRepository<MaterialType> materialTypeRepository) =>
                {
                    return await HandleAsync(new DeleteMaterialTypeRequest(materialTypeId), materialTypeRepository);
                })
            .Produces<DeleteMaterialTypeResponse>()
            .WithTags("MaterialTypeEndpoints");
    }

    public async Task<IResult> HandleAsync(DeleteMaterialTypeRequest request, IRepository<MaterialType> materialTypeRepository)
    {
        var response = new DeleteMaterialTypeResponse(request.CorrelationId());

        var materialTypeToDelete = await materialTypeRepository.GetByIdAsync(request.MaterialTypeId);
        if (materialTypeToDelete is null)
            return Results.NotFound();

        await materialTypeRepository.DeleteAsync(materialTypeToDelete);

        return Results.Ok(response);
    }
}
