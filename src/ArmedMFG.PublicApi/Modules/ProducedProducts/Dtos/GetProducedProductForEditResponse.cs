using System;
using ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos;

public class GetProducedProductForEditResponse : BaseResponse
{
    public GetProducedProductForEditResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetProducedProductForEditResponse()
    {
    }

    public ProducedProductForEditDto? ProducedProduct { get; set; }
}
