using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.Products.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Products.Dtos;

public class GetProductOptionsResponse : BaseResponse
{
    public GetProductOptionsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetProductOptionsResponse()
    {
    }

    public List<ProductOptionDto> ProductOptions { get; set; } = new List<ProductOptionDto>();
}
