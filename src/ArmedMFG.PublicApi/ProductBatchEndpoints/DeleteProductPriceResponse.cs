using System;

namespace ArmedMFG.PublicApi.ProductTypeEndpoints.ProductPriceEndpoints;

public class DeleteProductPriceResponse : BaseResponse
{
    public DeleteProductPriceResponse(Guid correlationId)
        : base()
    {
    }

    public DeleteProductPriceResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
