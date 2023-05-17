using System;
using ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos.SharedDtos;
using ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.ProducedProducts.Dtos;

public class UpdateProducedProductResponse : BaseResponse
{
    public UpdateProducedProductResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateProducedProductResponse()
    {
    }

    public ProducedProductDto? ProducedProduct { get; set; }
}
