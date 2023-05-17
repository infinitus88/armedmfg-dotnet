using System;
using ArmedMFG.PublicApi.Modules.Products.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Products.Dtos;

public class UpdateProductResponse : BaseResponse
{
    public UpdateProductResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateProductResponse()
    {
    }

    public ProductDto? Product { get; set; }
}
