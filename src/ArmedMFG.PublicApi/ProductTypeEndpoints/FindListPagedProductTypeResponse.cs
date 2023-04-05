using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.ProductBatchEndpoints;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class FindListPagedProductTypeResponse : BaseResponse
{
    public FindListPagedProductTypeResponse(Guid correlationId) : base(correlationId)
    {
    }

    public FindListPagedProductTypeResponse()
    {
    }

    public List<ProductTypeInfoDto> ProductTypes { get; set; } = new List<ProductTypeInfoDto>();
    public int TotalCount { get; set; }
}
