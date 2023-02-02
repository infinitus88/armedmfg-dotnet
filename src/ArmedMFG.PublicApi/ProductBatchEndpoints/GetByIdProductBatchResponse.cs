using System;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class GetByIdProductBatchResponse : BaseResponse
{
    public GetByIdProductBatchResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdProductBatchResponse()
    {
    }

    public ProductBatchDto ProductBatch { get; set; }
}
