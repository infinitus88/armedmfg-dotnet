using System;
using ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos;

public class CreateProductMaterialResponse : BaseResponse
{
    public CreateProductMaterialResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateProductMaterialResponse()
    {
    }

    public ProductMaterialDto ProductMaterial { get; set; }
}
