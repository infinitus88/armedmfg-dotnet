using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class GetByIdProductTypeEndpoint : IEndpoint<IResult, GetByIdProductTypeRequest, IRepository<ProductType>>
{
    private readonly IUriComposer _uriComposer;

    public GetByIdProductTypeEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/product-type/{productTypeId}",
                async (int productTypeId, IRepository<ProductType> productTypeRepository) =>
                {
                    return await HandleAsync(new GetByIdProductTypeRequest(productTypeId), productTypeRepository);
                })
            .Produces<GetByIdProductTypeResponse>()
            .WithTags("ProductTypeEnpoints");
    }
    
    public async Task<IResult> HandleAsync(GetByIdProductTypeRequest request, IRepository<ProductType> productTypeRepository)
    {
        var response = new GetByIdProductTypeResponse(request.CorrelationId());

        var productType = await productTypeRepository.GetByIdAsync(request.ProductTypeId);
        if (productType is null)
            return Results.NotFound();

        response.ProductType = new ProductTypeDto
        {
            Id = productType.Id,
            ProductCategoryId = productType.ProductCategoryId,
            Description = productType.Description,
            Name = productType.Name,
            PictureUri = _uriComposer.ComposePicUri(productType.PictureUri),
        };

        return Results.Ok(response);
    }
}
