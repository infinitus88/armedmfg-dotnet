using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.Products.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.Products.Dtos;

public class SearchProductResponse : BaseResponse
{
    public SearchProductResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchProductResponse()
    {
    }

    public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    public int TotalCount { get; set; }
}
