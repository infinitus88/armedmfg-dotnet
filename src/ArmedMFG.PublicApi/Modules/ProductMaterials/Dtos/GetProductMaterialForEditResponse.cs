using System;
using ArmedMFG.PublicApi.Modules.Customers.Dtos.Shared;
using ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos;

public class GetProductMaterialForEditResponse : BaseResponse
{
    public GetProductMaterialForEditResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetProductMaterialForEditResponse()
    {
    }

    public ProductMaterialForEditDto? ProductMaterial { get; set; }
}
