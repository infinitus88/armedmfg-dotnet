using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class GetProductOptionsEndpoint : IEndpoint<IResult, IRepository<ProductType>>
{
    private readonly IMapper _mapper;

    public GetProductOptionsEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/product-types/input-options",
                async (IRepository<ProductType> productsRepository) =>
                {
                    return await HandleAsync(productsRepository);
                })
            .Produces<GetProductOptionsResponse>()
            .WithTags("ProductTypesEndpoints");
    }
    
    public async Task<IResult> HandleAsync(IRepository<ProductType> productsRepository)
    {
        // await Task.Delay(1000);
        var response = new GetProductOptionsResponse();

        var products = await productsRepository.ListAsync();

        foreach (var product in products)
        {
            response.Products.Add(new ProductTypeOptionDto() { Id = product.Id, Name = product.Name, Price = product.GetCurrentPrice() });
        }

        return Results.Ok(response);
    } 
}
