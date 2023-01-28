using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class ListPagedProductPriceResponse : BaseResponse
{
    public ListPagedProductPriceResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListPagedProductPriceResponse()
    {
    }

    public List<ProductPriceDto> ProductPrices { get; set; } = new List<ProductPriceDto>();
    public int PageCount {get; set; }
}
