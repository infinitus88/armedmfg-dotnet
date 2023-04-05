using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductStockAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.MaterialTypeEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductStockEndpoints.ProductCheckpointEndpoints;

public class CreateProductCheckPointEndpoint : IEndpoint<IResult, CreateProductCheckPointRequest, IRepository<ProductCheckPoint>>
{
    public CreateProductCheckPointEndpoint()
    {
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product-stocks/check-points",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (CreateProductCheckPointRequest request, IRepository<ProductCheckPoint> checkPointRepository) =>
                {
                    return await HandleAsync(request, checkPointRepository);
                })
            .Produces<CreateMaterialTypeResponse>()
            .WithTags("ProductStockEndpoints");
    }

    public async Task<IResult> HandleAsync(CreateProductCheckPointRequest request, IRepository<ProductCheckPoint> checkPoinRepository)
    {
        var response = new CreateProductCheckPointResponse(request.CorrelationId());

        var newItem = new ProductCheckPoint(request.Data.CheckedDate, request.Data.ProductTypeId, request.Data.Quantity);
        newItem = await checkPoinRepository.AddAsync(newItem);

        var dto = new ProductCheckPointDto
        {
            Id = newItem.Id,
            CheckedDate = newItem.CheckedDate,
            ProductTypeId = newItem.ProductTypeId,
            ProductName = newItem.ProductType.Name,
            ProductCategoryId = newItem.ProductType.ProductCategoryId,
        };
        response.ProductCheckPoint = dto;
        return Results.Created($"api/product-stocks/check-points/{dto.Id}", response);
    }
}
