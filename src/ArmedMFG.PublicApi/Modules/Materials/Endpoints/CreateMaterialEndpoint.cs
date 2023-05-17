using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using ArmedMFG.ApplicationCore.Exceptions;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications.Materials;
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

public class CreateMaterialEndpoint : IEndpoint<IResult, CreateMaterialRequest>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Material> _materialRepository;
    public CreateMaterialEndpoint(IMapper mapper, IRepository<Material> materialRepository)
    {
        _mapper = mapper;
        _materialRepository = materialRepository;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/materials",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (CreateMaterialRequest request) =>
                {
                    return await HandleAsync(request);
                })
            .Produces<CreateMaterialResponse>()
            .WithTags("MaterialEndpoints");
    }

    public async Task<IResult> HandleAsync(CreateMaterialRequest request)
    {
        var response = new CreateMaterialResponse(request.CorrelationId());

        var materialNameSpecification = new MaterialNameSpecification(request.Name);
        var existingMaterial = await _materialRepository.CountAsync(materialNameSpecification);
        if (existingMaterial > 0)
        {
            throw new DuplicateException($"A material with name {request.Name} already exists");
        }

        var newItem = new Material(request.MaterialCategoryId, request.Name, (Unit)request.Unit, request.Amount);
        newItem = await _materialRepository.AddAsync(newItem);

        response.Material = _mapper.Map<MaterialDto>(newItem);

        return Results.Created($"api/materials/{newItem.Id}", response);
    }
}
