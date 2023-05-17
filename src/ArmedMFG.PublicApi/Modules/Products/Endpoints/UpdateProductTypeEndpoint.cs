using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.Modules.Products.Dtos;
using ArmedMFG.PublicApi.Modules.Products.Dtos.SharedDtos;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.Products.Endpoints;

public class UpdateProductEndpoint : IEndpoint<IResult, UpdateProductRequest>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Product> _productRepository;

    public UpdateProductEndpoint(IMapper mapper, IRepository<Product> productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/products",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (UpdateProductRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<UpdateProductResponse>()
            .WithTags("ProductEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateProductRequest request)
    {
        var response = new UpdateProductResponse(request.CorrelationId());

        var existingProduct = await _productRepository.GetByIdAsync(request.Id);

        if (existingProduct is null)
            return Results.NotFound();

        Product.ProductDetails details = new(request.Name, request.ProductCategoryId, request.UnitPrice);
        existingProduct.UpdateDetails(details);

        await _productRepository.UpdateAsync(existingProduct);

        response.Product = _mapper.Map<ProductDto>(existingProduct);
        return Results.Ok(response);
    }
}
