using System.Linq;
using System.Threading.Tasks;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using ArmedMFG.ApplicationCore.Exceptions;
using ArmedMFG.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using MinimalApi.Endpoint;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class UpdateProductBatchEndpoint : IEndpoint<IResult, UpdateProductBatchRequest, IRepository<ProductBatch>>
{
    private readonly IMapper _mapper;
    
    public UpdateProductBatchEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/product-batches",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (UpdateProductBatchRequest request, IRepository<ProductBatch> productBatchRepository) =>
                {
                    return await HandleAsync(request, productBatchRepository);
                })
            .Produces<UpdateProductBatchResponse>()
            .WithTags("ProductBatchEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateProductBatchRequest request, IRepository<ProductBatch> productBatchRepository)
    {
        var response = new UpdateProductBatchResponse(request.CorrelationId());

        var existingProductBatch = await productBatchRepository.GetByIdAsync(request.Id);

        if (existingProductBatch is null)
        {
            throw new NotFoundException($"A productBatch with Id : {request.Id} is not found");
        }

        ProductBatch.ProductBatchDetails details = new(request.ProducedDate);
        existingProductBatch.UpdateDetails(details);
        
        request.ProducedProducts.ForEach(p =>
            existingProductBatch.UpdateProduct(p.Id,
                new ProducedProduct.ProducedProductDetails(p.ProductTypeId, p.Quantity)));
        
        request.SpentMaterials.ForEach(sm => 
            existingProductBatch.UpdateMaterial(sm.Id,
                new SpentMaterial.SpentMaterialDetails(sm.MaterialTypeId, sm.Amount)));
        
        
        await productBatchRepository.UpdateAsync(existingProductBatch);

        var dto = new ProductBatchDto
        {
            Id = existingProductBatch.Id,
            ProducedDate = existingProductBatch.ProducedDate
        };
        response.ProductBatch = dto;
        response.ProductBatch.ProducedProducts.AddRange(
            existingProductBatch.ProducedProducts.Select(_mapper.Map<ProducedProductDto>));
        response.ProductBatch.SpentMaterials.AddRange(
            existingProductBatch.SpentMaterials.Select(_mapper.Map<SpentMaterialDto>));
        return Results.Ok(response);
    }
}
