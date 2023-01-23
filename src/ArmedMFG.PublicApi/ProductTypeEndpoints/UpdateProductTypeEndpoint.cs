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

public class UpdateProductTypeEndpoint : IEndpoint<IResult, UpdateProductTypeRequest, IRepository<ProductType>>
{
    private readonly IUriComposer _uriComposer;

    public UpdateProductTypeEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/product-types",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (UpdateProductTypeRequest request, IRepository<ProductType> productTypeRepository) =>
                {
                    return await HandleAsync(request, productTypeRepository);
                })
            .Produces<UpdateProductTypeResponse>()
            .WithTags("ProductTypeEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateProductTypeRequest request, IRepository<ProductType> productTypeRepository)
    {
        var response = new UpdateProductTypeResponse(request.CorrelationId());

        var existingProductType = await productTypeRepository.GetByIdAsync(request.Id);

        ProductType.ProductTypeDetails details = new(request.Name, request.Description);
        existingProductType.UpdateDetails(details);
        existingProductType.UpdateCategory(request.ProductCategoryId);

        await productTypeRepository.UpdateAsync(existingProductType);

        var dto = new ProductTypeDto
        {
            Id = existingProductType.Id,
            ProductCategoryId = existingProductType.ProductCategoryId,
            Description = existingProductType.Description,
            Name = existingProductType.Name,
            PictureUri = _uriComposer.ComposePicUri(existingProductType.PictureUri),
            CurrentPrice = existingProductType.GetCurrentPrice()
        };
        response.ProductType = dto;
        return Results.Ok(response);
    }
}
