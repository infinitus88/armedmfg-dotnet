using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class GetMaterialOptionsEndpoint : IEndpoint<IResult, IRepository<MaterialType>>
{
    private readonly IMapper _mapper;

    public GetMaterialOptionsEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/material-types/input-options",
                async (IRepository<MaterialType> materialsRepository) =>
                {
                    return await HandleAsync(materialsRepository);
                })
            .Produces<GetMaterialOptionsResponse>()
            .WithTags("MaterialTypesEndpoints");
    }
    
    public async Task<IResult> HandleAsync(IRepository<MaterialType> materialsRepository)
    {
        // await Task.Delay(1000);
        var response = new GetMaterialOptionsResponse();

        var products = await materialsRepository.ListAsync();

        foreach (var product in products)
        {
            response.Materials.Add(new MaterialTypeOptionDto() { Id = product.Id, Name = product.Name });
        }

        return Results.Ok(response);
    } 
}
