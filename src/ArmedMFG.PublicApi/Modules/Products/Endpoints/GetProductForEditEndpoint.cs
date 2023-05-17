using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.Modules.Products.Dtos;
using ArmedMFG.PublicApi.Modules.Products.Dtos.SharedDtos;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.Products.Endpoints;

public class GetProductForEditEndpoint : IEndpoint<IResult, GetProductForEditRequest>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductForEditEndpoint(IMapper mapper, IRepository<Product> productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<IResult> HandleAsync(GetProductForEditRequest request)
    {
        var response = new GetProductForEditResponse(request.CorrelationId());

        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product is null)
            return Results.NotFound();

        response.ProductForEdit = _mapper.Map<ProductForEditDto>(product);

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products/{productId}",
                async (int productId) =>
                {
                    return await HandleAsync(new GetProductForEditRequest(productId));
                })
            .Produces<GetProductForEditResponse>()
            .WithTags("ProductEndpoints");
    }
}
