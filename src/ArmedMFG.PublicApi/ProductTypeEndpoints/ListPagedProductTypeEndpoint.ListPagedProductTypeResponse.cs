using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class ListPagedProductTypeResponse : BaseResponse
{
    public ListPagedProductTypeResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListPagedProductTypeResponse()
    {
    }

    public List<ProductTypeDto> ProductTypes { get; set; } = new List<ProductTypeDto>();
    public int PageCount {get; set; }
}
