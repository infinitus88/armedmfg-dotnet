using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.Modules.Materials.Dtos;
using ArmedMFG.PublicApi.Modules.Materials.Dtos.SharedDtos;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.Materials.Endpoints;

public class GetMaterialOptionsEndpoint : IEndpoint<IResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Material> _materialRepository;

    public GetMaterialOptionsEndpoint(IMapper mapper, IRepository<Material> materialRepository)
    {
        _mapper = mapper;
        _materialRepository = materialRepository;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/materials/options", async () => await HandleAsync())
            .Produces<GetMaterialOptionsResponse>()
            .WithTags("MaterialEndpoints");
    }

    public async Task<IResult> HandleAsync()
    {
        var response = new GetMaterialOptionsResponse();

        var materials = await _materialRepository.ListAsync();

        response.MaterialOptions.AddRange(materials.Select(_mapper.Map<MaterialOptionDto>));

        return Results.Ok(response);
    }
}
