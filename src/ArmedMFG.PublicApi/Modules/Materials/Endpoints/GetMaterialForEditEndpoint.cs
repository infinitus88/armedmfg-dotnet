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

public class GetMaterialForEditEndpoint : IEndpoint<IResult, GetMaterialForEditRequest>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Material> _materialRepository;

    public GetMaterialForEditEndpoint(IMapper mapper, IRepository<Material> materialRepository)
    {
        _mapper = mapper;
        _materialRepository = materialRepository;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/materials/{materialId}",
                async (int materialId) =>
                {
                    return await HandleAsync(new GetMaterialForEditRequest(materialId));
                })
            .Produces<GetMaterialForEditResponse>()
            .WithTags("MaterialEndpoints");
    }

    public async Task<IResult> HandleAsync(GetMaterialForEditRequest request)
    {
        var response = new GetMaterialForEditResponse(request.CorrelationId());

        var material = await _materialRepository.GetByIdAsync(request.MaterialId);
        if (material is null)
            return Results.NotFound();

        response.MaterialForEdit = _mapper.Map<MaterialForEditDto>(material);

        return Results.Ok(response);
    }
}
