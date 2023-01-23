using System;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints;

public class DeleteProductTypeResponse : BaseResponse
{
    public DeleteProductTypeResponse(Guid correlationId)
        : base()
    {
    }

    public DeleteProductTypeResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
