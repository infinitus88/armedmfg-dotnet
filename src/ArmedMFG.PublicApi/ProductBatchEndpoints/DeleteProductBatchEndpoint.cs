using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.ProductTypeEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class DeleteProductBatchEndpoint : IEndpoint<IResult, DeleteProductBatchRequest, IRepository<ProductBatch>>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/product-batch/{productBatchId}",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (int productBatchId, IRepository<ProductBatch> productBatchRepository) =>
                {
                    return await HandleAsync(new DeleteProductBatchRequest(productBatchId), productBatchRepository);
                })
            .Produces<DeleteProductBatchResponse>()
            .WithTags("ProductBatchEndpoints");
    }

    public async Task<IResult> HandleAsync(DeleteProductBatchRequest request, IRepository<ProductBatch> productBatchRepository)
    {
        var response = new DeleteProductBatchResponse(request.CorrelationId());

        var productBatchToDelete = await productBatchRepository.GetByIdAsync(request.ProductBatchId);
        if (productBatchToDelete is null)
            return Results.NotFound();

        await productBatchRepository.DeleteAsync(productBatchToDelete);

        return Results.Ok(response);
    }
}
