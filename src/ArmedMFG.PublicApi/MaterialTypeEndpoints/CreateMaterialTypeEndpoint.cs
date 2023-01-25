using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Exceptions;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints;

public class CreateMaterialTypeEndpoint : IEndpoint<IResult, CreateMaterialTypeRequest, IRepository<MaterialType>>
{
    public CreateMaterialTypeEndpoint()
    {
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/material-types",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (CreateMaterialTypeRequest request, IRepository<MaterialType> materialTypeRepository) =>
                {
                    return await HandleAsync(request, materialTypeRepository);
                })
            .Produces<CreateMaterialTypeResponse>()
            .WithTags("MaterialTypeEndpoints");
    }

    public async Task<IResult> HandleAsync(CreateMaterialTypeRequest request, IRepository<MaterialType> materialTypeRepository)
    {
        var response = new CreateMaterialTypeResponse(request.CorrelationId());

        var materialTypeNameSpecification = new MaterialTypeNameSpecification(request.Name);
        var existingMaterialType = await materialTypeRepository.CountAsync(materialTypeNameSpecification);
        if (existingMaterialType > 0)
        {
            throw new DuplicateException($"A materialType with name {request.Name} already exists");
        }

        var newItem = new MaterialType(request.MaterialCategoryId, request.Name, request.Description, request.CurrentAmount);
        newItem = await materialTypeRepository.AddAsync(newItem);

        var dto = new MaterialTypeDto
        {
            Id = newItem.Id,
            MaterialCategoryId = newItem.MaterialCategoryId,
            Description = newItem.Description,
            Name = newItem.Name,
            CurrentAmount = newItem.GetCurrentAmount()
        };
        response.MaterialType = dto;
        return Results.Created($"api/material-types/{dto.Id}", response);
    }
}
