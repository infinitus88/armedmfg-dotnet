using System;
using ArmedMFG.PublicApi.Modules.Products.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Products.Dtos;

public class CreateProductResponse : BaseResponse
{
    public CreateProductResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateProductResponse()
    {
    }

    public ProductDto Product { get; set; }
}
