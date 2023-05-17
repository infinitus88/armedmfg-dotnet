using System;
using ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos;

public class CreateProducedProductResponse : BaseResponse
{
    public CreateProducedProductResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateProducedProductResponse()
    {
    }

    public ProducedProductDto ProducedProduct { get; set; }
}
