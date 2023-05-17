using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.Modules.Products.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.Products.Endpoints;

public class DeleteSingleProductEndpoint : IEndpoint<IResult, DeleteSingleProductRequest>
{
    private readonly IRepository<Product> _productRepository;

    public DeleteSingleProductEndpoint(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }


    public async Task<IResult> HandleAsync(DeleteSingleProductRequest request)
    {
        var response = new DeleteSingleProductResponse(request.CorrelationId());

        var productToDelete = await _productRepository.GetByIdAsync(request.ProductId);
        if (productToDelete is null)
            return Results.NotFound();

        await _productRepository.DeleteAsync(productToDelete);

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/products/{productId}",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (int productId) =>
                {
                    return await HandleAsync(new DeleteSingleProductRequest(productId));
                })
            .Produces<DeleteSingleProductResponse>()
            .WithTags("ProductEndpoints");
    }
}
