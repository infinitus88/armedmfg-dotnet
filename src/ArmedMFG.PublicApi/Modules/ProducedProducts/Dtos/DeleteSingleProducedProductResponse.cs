using System;

namespace ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos;

public class DeleteSingleProducedProductResponse : BaseResponse
{
    public DeleteSingleProducedProductResponse(Guid correlationId) : base(correlationId)
    {
    }

    public DeleteSingleProducedProductResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
