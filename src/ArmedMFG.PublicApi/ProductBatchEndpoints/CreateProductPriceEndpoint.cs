using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Exceptions;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class CreateProductPriceEndpoint : IEndpoint<IResult, CreateProductPriceRequest, IRepository<ProductPrice>, IRepository<ProductType>>
{
    private readonly IMapper _mapper;

    public CreateProductPriceEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product-types/prices",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS,
                    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                async
                    (CreateProductPriceRequest request, IRepository<ProductPrice> productPriceRepository, IRepository<ProductType> productTypeRepository) =>
                {
                    return await HandleAsync(request, productPriceRepository, productTypeRepository);
                })
            .Produces<CreateProductPriceResponse>()
            .WithTags("ProductPriceEndpoints");
    }
    
    public async Task<IResult> HandleAsync(CreateProductPriceRequest request,
        IRepository<ProductPrice> productPriceRepository, IRepository<ProductType> productTypeRepository)
    {
        var response = new CreateProductPriceResponse(request.CorrelationId());
        
        // var productPriceNameSpecification = new ProductPrice
        var existingProductType = await productTypeRepository.GetByIdAsync(request.ProductTypeId);
        if (existingProductType == null)
        {
            throw new NotFoundException($"A productType with Id: {request.ProductTypeId} not be found");
        }

        var newPrice = new ProductPrice(existingProductType.Id, request.FromDate, request.Price);
        newPrice = await productPriceRepository.AddAsync(newPrice);

        var dto = new ProductPriceDto
        {
            Id = newPrice.Id,
            ProductTypeId = newPrice.ProductTypeId,
            FromDate = newPrice.FromDate,
            Price = newPrice.Price
        };
        response.ProductPrice = dto;
        return Results.Created($"api/product-types/prices/{dto.Id}", response);
    }
}
