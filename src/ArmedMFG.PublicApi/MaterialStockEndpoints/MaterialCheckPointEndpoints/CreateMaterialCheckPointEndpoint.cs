using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialStockAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.MaterialTypeEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.MaterialStockEndpoints.MaterialCheckPointEndpoints;

public class CreateMaterialCheckPointEndpoint : IEndpoint<IResult, CreateMaterialCheckPointRequest, IRepository<MaterialCheckPoint>>
{
    public CreateMaterialCheckPointEndpoint()
    {
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/material-stocks/check-points",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (CreateMaterialCheckPointRequest request, IRepository<MaterialCheckPoint> checkPointRepository) =>
                {
                    return await HandleAsync(request, checkPointRepository);
                })
            .Produces<CreateMaterialTypeResponse>()
            .WithTags("MaterialStockEndpoints");
    }

    public async Task<IResult> HandleAsync(CreateMaterialCheckPointRequest request, IRepository<MaterialCheckPoint> checkPoinRepository)
    {
        var response = new CreateMaterialCheckPointResponse(request.CorrelationId());

        var newItem = new MaterialCheckPoint(request.Data.CheckedDate, request.Data.MaterialTypeId, request.Data.Amount);
        newItem = await checkPoinRepository.AddAsync(newItem);

        var dto = new MaterialCheckPointDto
        {
            Id = newItem.Id,
            CheckedDate = newItem.CheckedDate,
            MaterialTypeId = newItem.MaterialTypeId,
            MaterialName = newItem.MaterialType.Name,
            MaterialCategoryId = newItem.MaterialType.MaterialCategoryId,
        };
        response.MaterialCheckPoint = dto;
        return Results.Created($"api/material-stocks/check-points/{dto.Id}", response);
    }
}
