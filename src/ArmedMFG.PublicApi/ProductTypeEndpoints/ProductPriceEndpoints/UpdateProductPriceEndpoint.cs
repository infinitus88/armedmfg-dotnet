using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class UpdateProductPriceEndpoint : IEndpoint<IResult, UpdateProductPriceRequest, IRepository<ProductPrice>>
{
    private readonly IMapper _mapper;
    
    public UpdateProductPriceEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/product-types/prices",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (UpdateProductPriceRequest request, IRepository<ProductPrice> productPriceRepository) =>
                {
                    return await HandleAsync(request, productPriceRepository);
                })
            .Produces<UpdateProductPriceResponse>()
            .WithTags("ProductPriceEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateProductPriceRequest request, IRepository<ProductPrice> productPriceRepository)
    {
        var response = new UpdateProductPriceResponse(request.CorrelationId());

        var existingProductPrice = await productPriceRepository.GetByIdAsync(request.Id);

        ProductPrice.ProductPriceDetails details = new(request.FromDate, request.Price);
        existingProductPrice.UpdateDetails(details);

        await productPriceRepository.UpdateAsync(existingProductPrice);

        var dto = new ProductPriceDto
        {
            Id = existingProductPrice.Id,
            ProductTypeId = existingProductPrice.ProductTypeId,
            FromDate = existingProductPrice.FromDate,
            Price = existingProductPrice.Price
        };
        response.ProductPrice = dto;
        return Results.Ok(response);
    }
}
