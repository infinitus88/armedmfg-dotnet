using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class DeleteMaterialSupplyEndpoint : IEndpoint<IResult, DeleteMaterialSupplyRequest, IRepository<MaterialSupply>>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/material-types/supplies/{materialSupplyId}",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (int materialSupplyId, IRepository<MaterialSupply> materialSupplyRepository) =>
                {
                    return await HandleAsync(new DeleteMaterialSupplyRequest(materialSupplyId), materialSupplyRepository);
                })
            .Produces<DeleteMaterialSupplyResponse>()
            .WithTags("MaterialSupplyEndpoints");
    }

    public async Task<IResult> HandleAsync(DeleteMaterialSupplyRequest request, IRepository<MaterialSupply> materialSupplyRepository)
    {
        var response = new DeleteMaterialSupplyResponse(request.CorrelationId());

        var materialSupplyToDelete = await materialSupplyRepository.GetByIdAsync(request.MaterialSupplyId);
        if (materialSupplyToDelete is null)
            return Results.NotFound();

        await materialSupplyRepository.DeleteAsync(materialSupplyToDelete);

        return Results.Ok(response);
    }
}
