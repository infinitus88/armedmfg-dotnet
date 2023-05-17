using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.Modules.Materials.Dtos;
using ArmedMFG.PublicApi.Modules.Materials.Dtos.SharedDtos;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.Materials.Endpoints;

public class UpdateMaterialEndpoint : IEndpoint<IResult, UpdateMaterialRequest>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Material> _materialRepository;
    public UpdateMaterialEndpoint(IMapper mapper, IRepository<Material> materialRepository)
    {
        _mapper = mapper;
        _materialRepository = materialRepository;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/materials",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (UpdateMaterialRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<UpdateMaterialResponse>()
            .WithTags("MaterialEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateMaterialRequest request)
    {
        var response = new UpdateMaterialResponse(request.CorrelationId());

        var existingMaterial = await _materialRepository.GetByIdAsync(request.Id);

        if (existingMaterial is null)
            return Results.NotFound();

        Material.MaterialDetails details = new(request.Name, (Unit)request.Unit, request.MaterialCategoryId);
        existingMaterial.UpdateDetails(details);

        await _materialRepository.UpdateAsync(existingMaterial);

        response.Material = _mapper.Map<MaterialDto>(existingMaterial);

        return Results.Ok(response);
    }
}
