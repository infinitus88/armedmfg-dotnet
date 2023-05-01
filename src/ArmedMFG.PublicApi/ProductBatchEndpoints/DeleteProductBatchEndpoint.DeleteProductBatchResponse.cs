using System;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class DeleteProductBatchResponse : BaseResponse
{
    public DeleteProductBatchResponse(Guid correlationId)
        : base()
    {
    }

    public DeleteProductBatchResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
