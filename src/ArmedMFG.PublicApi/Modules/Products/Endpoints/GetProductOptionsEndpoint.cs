using System.Linq;
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

public class GetProductOptionsEndpoint : IEndpoint<IResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Product> _productRepository;

    public GetProductOptionsEndpoint(IMapper mapper, IRepository<Product> productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products/options", async () => await HandleAsync())
            .Produces<GetProductOptionsResponse>()
            .WithTags("ProductEndpoints");
    }

    public async Task<IResult> HandleAsync()
    {
        var response = new GetProductOptionsResponse();

        var products = await _productRepository.ListAsync();

        response.ProductOptions.AddRange(products.Select(_mapper.Map<ProductOptionDto>));

        return Results.Ok(response);
    }
}
