using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.ProductCategoryEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.ProductCategoryEndpoints;

/// <summary>
/// List Product Category
/// </summary>
public class ProductCategoryListEndpoint : IEndpoint<IResult, IRepository<ProductCategory>>
{
    private readonly IMapper _mapper;

    public ProductCategoryListEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/product-categories",
            async (IRepository<ProductCategory> productCategoryRepository) =>
            {
                return await HandleAsync(productCategoryRepository);
            })
            .Produces<ListProductCategoriesResponse>()
            .WithTags("ProductCategoryEndpoints");
    }

    public async Task<IResult> HandleAsync(IRepository<ProductCategory> productCategoryRepository)
    {
        var response = new ListProductCategoriesResponse();

        var categories = await productCategoryRepository.ListAsync();

        response.ProductCategories.AddRange(categories.Select(_mapper.Map<ProductCategoryDto>));

        return Results.Ok(response);
    }
}
