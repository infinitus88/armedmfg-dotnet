using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class DeleteProductTypeEndpoint : IEndpoint<IResult, DeleteProductTypeRequest, IRepository<ProductType>>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/product-types/{productTypeId}",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (int productTypeId, IRepository<ProductType> productTypeRepository) =>
                {
                    return await HandleAsync(new DeleteProductTypeRequest(productTypeId), productTypeRepository);
                })
            .Produces<DeleteProductTypeResponse>()
            .WithTags("ProductTypeEndpoints");
    }

    public async Task<IResult> HandleAsync(DeleteProductTypeRequest request, IRepository<ProductType> productTypeRepository)
    {
        var response = new DeleteProductTypeResponse(request.CorrelationId());

        var productTypeToDelete = await productTypeRepository.GetByIdAsync(request.ProductTypeId);
        if (productTypeToDelete is null)
            return Results.NotFound();

        await productTypeRepository.DeleteAsync(productTypeToDelete);

        return Results.Ok(response);
    }
}
