using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Endpoints;

public class DeleteSingleMaterialSupplyEndpoint : IEndpoint<IResult, DeleteSingleMaterialSupplyRequest>
{
    private readonly IRepository<MaterialSupply> _materialSupplyRepository;

    public DeleteSingleMaterialSupplyEndpoint(IRepository<MaterialSupply> materialSupplyRepository)
    {
        _materialSupplyRepository = materialSupplyRepository;
    }


    public async Task<IResult> HandleAsync(DeleteSingleMaterialSupplyRequest request)
    {
        var response = new DeleteSingleMaterialSupplyResponse(request.CorrelationId());

        var materialSupplyToDelete = await _materialSupplyRepository.GetByIdAsync(request.MaterialSupplyId);
        if (materialSupplyToDelete is null)
            return Results.NotFound();

        await _materialSupplyRepository.DeleteAsync(materialSupplyToDelete);

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/materials/supplies/{materialSupplyId}",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (int materialSupplyId) =>
                {
                    return await HandleAsync(new DeleteSingleMaterialSupplyRequest(materialSupplyId));
                })
            .Produces<DeleteSingleMaterialSupplyResponse>()
            .WithTags("MaterialSupplyEndpoints");
    }
}
