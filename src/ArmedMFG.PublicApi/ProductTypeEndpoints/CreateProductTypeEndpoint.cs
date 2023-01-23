using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Exceptions;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class CreateProductTypeEndpoint : IEndpoint<IResult, CreateProductTypeRequest, IRepository<ProductType>>
{
    private readonly IUriComposer _uriComposer;

    public CreateProductTypeEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product-types",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (CreateProductTypeRequest request, IRepository<ProductType> productTypeRepository) =>
                {
                    return await HandleAsync(request, productTypeRepository);
                })
            .Produces<CreateProductTypeResponse>()
            .WithTags("ProductTypeEndpoints");
    }

    public async Task<IResult> HandleAsync(CreateProductTypeRequest request, IRepository<ProductType> productTypeRepository)
    {
        var response = new CreateProductTypeResponse(request.CorrelationId());

        var productTypeNameSpecification = new ProductTypeNameSpecification(request.Name);
        var existingProductType = await productTypeRepository.CountAsync(productTypeNameSpecification);
        if (existingProductType > 0)
        {
            throw new DuplicateException($"A productType with name {request.Name} already exists");
        }

        var newItem = new ProductType(request.ProductCategoryId, request.Name, request.Description, request.PictureUri, request.CurrentPrice);
        newItem = await productTypeRepository.AddAsync(newItem);

        if (newItem.Id != 0)
        {
            // TODO Upload functionality disabled by adding default/placeholder image due to a potential security risk
            // pointed out by the community from issue: https://github.com/dotnet-architecture/eShopOnWeb/issues/537 
            // recommended fix is firstly upload picture to a blob storage and deliver the image via CDN after a verification process.
            newItem.UpdatePictureUri("eCatalog-item-default.png");
            await productTypeRepository.UpdateAsync(newItem);
        }

        var dto = new ProductTypeDto
        {
            Id = newItem.Id,
            ProductCategoryId = newItem.ProductCategoryId,
            Description = newItem.Description,
            Name = newItem.Name,
            PictureUri = _uriComposer.ComposePicUri(newItem.PictureUri),
            CurrentPrice = newItem.GetCurrentPrice()
        };
        response.ProductType = dto;
        return Results.Created($"api/product-types/{dto.Id}", response);
    }
}
