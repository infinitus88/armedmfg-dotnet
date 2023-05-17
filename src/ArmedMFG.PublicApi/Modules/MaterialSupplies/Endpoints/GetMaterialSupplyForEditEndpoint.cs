using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos;
using ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos.SharedDtos;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Endpoints;

public class GetMaterialSupplyForEditEndpoint : IEndpoint<IResult, GetMaterialSupplyForEditRequest>
{
    private readonly IMapper _mapper;
    private readonly IRepository<MaterialSupply> _materialSupplyRepository;

    public GetMaterialSupplyForEditEndpoint(IMapper mapper, IRepository<MaterialSupply> materialSupplyRepository)
    {
        _mapper = mapper;
        _materialSupplyRepository = materialSupplyRepository;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/materials/supplies/{materialSupplyId}",
                async (int materialSupplyId) =>
                {
                    return await HandleAsync(new GetMaterialSupplyForEditRequest(materialSupplyId));
                })
            .Produces<GetMaterialSupplyForEditResponse>()
            .WithTags("MaterialSupplyEndpoints");
    }

    public async Task<IResult> HandleAsync(GetMaterialSupplyForEditRequest request)
    {
        var response = new GetMaterialSupplyForEditResponse(request.CorrelationId());

        var materialSupply = await _materialSupplyRepository.GetByIdAsync(request.MaterialSupplyId);
        if (materialSupply is null)
            return Results.NotFound();

        response.MaterialSupplyForEdit = _mapper.Map<MaterialSupplyForEditDto>(materialSupply);

        return Results.Ok(response);
    }
}
