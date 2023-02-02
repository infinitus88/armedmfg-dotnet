using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class GetByIdProductPriceEndpoint : IEndpoint<IResult, GetByIdProductPriceRequest, IRepository<ProductPrice>>
{
    private readonly IUriComposer _uriComposer;

    public GetByIdProductPriceEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/product-types/prices/{productPriceId}",
                async (int productPriceId, IRepository<ProductPrice> productPriceRepository) =>
                {
                    return await HandleAsync(new GetByIdProductPriceRequest(productPriceId), productPriceRepository);
                })
            .Produces<GetByIdProductPriceResponse>()
            .WithTags("ProductPriceEndpoints");
    }
    
    public async Task<IResult> HandleAsync(GetByIdProductPriceRequest request, IRepository<ProductPrice> productPriceRepository)
    {
        var response = new GetByIdProductPriceResponse(request.CorrelationId());

        var productPrice = await productPriceRepository.GetByIdAsync(request.ProductPriceId);
        if (productPrice is null)
            return Results.NotFound();

        response.ProductPrice = new ProductPriceDto
        {
            Id = productPrice.Id,
            FromDate = productPrice.FromDate,
            ProductTypeId = productPrice.ProductTypeId,
            Price = productPrice.Price
        };

        return Results.Ok(response);
    }
}
