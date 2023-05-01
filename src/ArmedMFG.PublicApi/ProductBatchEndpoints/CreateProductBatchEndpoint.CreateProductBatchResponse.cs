using System;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class CreateProductBatchResponse : BaseResponse
{
    public CreateProductBatchResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateProductBatchResponse()
    {
    }

    public ProductBatchDto ProductBatch { get; set; }
}
