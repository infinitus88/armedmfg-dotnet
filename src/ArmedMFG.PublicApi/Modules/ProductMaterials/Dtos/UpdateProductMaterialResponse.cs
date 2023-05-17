using System;
using ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos;

public class UpdateProductMaterialResponse : BaseResponse
{
    public UpdateProductMaterialResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateProductMaterialResponse()
    {
    }

    public ProductMaterialDto? ProductMaterial { get; set; }
}
