using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class DeleteProductPriceEndpoint : IEndpoint<IResult, DeleteProductPriceRequest, IRepository<ProductPrice>>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/product-types/prices/{productPriceId}",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (int productPriceId, IRepository<ProductPrice> productPriceRepository) =>
                {
                    return await HandleAsync(new DeleteProductPriceRequest(productPriceId), productPriceRepository);
                })
            .Produces<DeleteProductTypeResponse>()
            .WithTags("ProductPriceEndpoints");
    }

    public async Task<IResult> HandleAsync(DeleteProductPriceRequest request, IRepository<ProductPrice> productPriceRepository)
    {
        var response = new DeleteProductPriceResponse(request.CorrelationId());

        var productPriceToDelete = await productPriceRepository.GetByIdAsync(request.ProductPriceId);
        if (productPriceToDelete is null)
            return Results.NotFound();

        await productPriceRepository.DeleteAsync(productPriceToDelete);

        return Results.Ok(response);
    }
}
