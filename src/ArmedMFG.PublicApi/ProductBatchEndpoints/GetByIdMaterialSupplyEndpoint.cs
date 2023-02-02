using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class GetByIdProductBatchEndpoint : IEndpoint<IResult, GetByIdProductBatchRequest, IRepository<ProductBatch>>
{
    private readonly IMapper _mapper;

    public GetByIdProductBatchEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/material-types/supplies/{productBatchId}",
                async (int productBatchId, IRepository<ProductBatch> productBatchRepository) =>
                {
                    return await HandleAsync(new GetByIdProductBatchRequest(productBatchId), productBatchRepository);
                })
            .Produces<GetByIdProductBatchResponse>()
            .WithTags("ProductBatchEndpoints");
    }
    
    public async Task<IResult> HandleAsync(GetByIdProductBatchRequest request, IRepository<ProductBatch> productBatchRepository)
    {
        var response = new GetByIdProductBatchResponse(request.CorrelationId());

        var productBatchWithListsSpec = new ProductBatchWithListsSpecification(request.ProductBatchId);

        var productBatch = await productBatchRepository.GetByIdAsync(productBatchWithListsSpec);
        if (productBatch is null)
            return Results.NotFound();
        
        response.ProductBatch = new ProductBatchDto
        {
            Id = productBatch.Id,
            ProducedDate = productBatch.ProducedDate
        };
        
        response.ProductBatch.ProducedProducts.AddRange(
            productBatch.ProducedProducts.Select(_mapper.Map<ProducedProductDto>));
        response.ProductBatch.SpentMaterials.AddRange(
            productBatch.SpentMaterials.Select(_mapper.Map<SpentMaterialDto>));

        return Results.Ok(response);
    }
}
