using System;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class UpdateProductBatchResponse : BaseResponse
{
    public UpdateProductBatchResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateProductBatchResponse()
    {
    }
    
    public ProductBatchDto ProductBatch { get; set; }
}
