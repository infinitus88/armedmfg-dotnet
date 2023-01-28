using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using ArmedMFG.ApplicationCore.Exceptions;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class CreateMaterialSupplyEndpoint : IEndpoint<IResult, CreateMaterialSupplyRequest, IRepository<MaterialSupply>, IRepository<MaterialType>>
{
    private readonly IMapper _mapper;

    public CreateMaterialSupplyEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/material-types/supplies",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS,
                    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                async
                    (CreateMaterialSupplyRequest request, IRepository<MaterialSupply> materialSupplyRepository, IRepository<MaterialType> materialTypeRepository) =>
                {
                    return await HandleAsync(request, materialSupplyRepository, materialTypeRepository);
                })
            .Produces<CreateProductPriceResponse>()
            .WithTags("MaterialSupplyEndpoints");
    }
    
    public async Task<IResult> HandleAsync(CreateMaterialSupplyRequest request,
        IRepository<MaterialSupply> materialSupplyRepository, IRepository<MaterialType> materialTypeRepository)
    {
        var response = new CreateMaterialSupplyResponse(request.CorrelationId());
        
        // var productPriceNameSpecification = new ProductPrice
        var existingMaterialType = await materialTypeRepository.GetByIdAsync(request.MaterialTypeId);
        if (existingMaterialType == null)
        {
            throw new NotFoundException($"A materialType with Id: {request.MaterialTypeId} not be found");
        }

        var newSupply = new MaterialSupply(existingMaterialType.Id, request.DeliveredDate, request.UnitPrice, request.Amount);
        newSupply = await materialSupplyRepository.AddAsync(newSupply);

        var dto = new MaterialSupplyDto
        {
            Id = newSupply.Id,
            MaterialTypeId = newSupply.MaterialTypeId,
            DeliveredDate = newSupply.DeliveredDate,
            UnitPrice = newSupply.UnitPrice,
            Amount = newSupply.Amount
        };
        response.MaterialSupply = dto;
        return Results.Created($"api/material-types/supplies/{dto.Id}", response);
    }
}
