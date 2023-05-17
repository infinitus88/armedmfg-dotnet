using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos;

public class SearchProducedProductResponse : BaseResponse
{
    public SearchProducedProductResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchProducedProductResponse()
    {
    }

    public List<ProducedProductDto> ProducedProducts { get; set; } = new List<ProducedProductDto>();
    public int TotalCount { get; set; }
}
