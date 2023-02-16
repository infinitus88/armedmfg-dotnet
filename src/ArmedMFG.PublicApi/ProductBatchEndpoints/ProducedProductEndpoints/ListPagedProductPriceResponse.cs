using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints.ProducedProductEndpoints;

public class ListPagedProducedProductResponse : BaseResponse
{
    public ListPagedProducedProductResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListPagedProducedProductResponse()
    {
    }

    public List<ProducedProductQuantityDto> ProducedProductsQuantity { get; set; } = new List<ProducedProductQuantityDto>();
}
