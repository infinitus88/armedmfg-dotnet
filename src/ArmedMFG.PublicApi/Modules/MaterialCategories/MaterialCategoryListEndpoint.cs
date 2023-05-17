using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.MaterialCategoryEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.MaterialCategories;

/// <summary>
/// List Material Category
/// </summary>
public class MaterialCategoryListEndpoint : IEndpoint<IResult, IRepository<MaterialCategory>>
{
    private readonly IMapper _mapper;

    public MaterialCategoryListEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/material-categories",
            async (IRepository<MaterialCategory> materialCategoryRepository) =>
            {
                return await HandleAsync(materialCategoryRepository);
            })
            .Produces<ListMaterialCategoriesResponse>()
            .WithTags("MaterialCategoryEndpoints");
    }

    public async Task<IResult> HandleAsync(IRepository<MaterialCategory> materialCategoryRepository)
    {
        var response = new ListMaterialCategoriesResponse();

        var categories = await materialCategoryRepository.ListAsync();

        response.MaterialCategories.AddRange(categories.Select(_mapper.Map<MaterialCategoryDto>));

        return Results.Ok(response);
    }
}
