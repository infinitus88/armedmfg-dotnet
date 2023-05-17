using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.Modules.Materials.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.Materials.Endpoints;

public class DeleteSingleMaterialEndpoint : IEndpoint<IResult, DeleteSingleMaterialRequest>
{
    private readonly IRepository<Material> _materialRepository;

    public DeleteSingleMaterialEndpoint(IRepository<Material> materialRepository)
    {
        _materialRepository = materialRepository;
    }


    public async Task<IResult> HandleAsync(DeleteSingleMaterialRequest request)
    {
        var response = new DeleteSingleMaterialResponse(request.CorrelationId());

        var materialToDelete = await _materialRepository.GetByIdAsync(request.MaterialId);
        if (materialToDelete is null)
            return Results.NotFound();

        await _materialRepository.DeleteAsync(materialToDelete);

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/materials/{materialId}",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (int materialId) =>
                {
                    return await HandleAsync(new DeleteSingleMaterialRequest(materialId));
                })
            .Produces<DeleteSingleMaterialResponse>()
            .WithTags("ProductEndpoints");
    }
}
