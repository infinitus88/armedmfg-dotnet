using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos.SharedDtos;

namespace ArmedMFG.PublicApi.Modules.ProductMaterials.Dtos;

public class SearchProductMaterialResponse : BaseResponse
{
    public SearchProductMaterialResponse(Guid correlationId) : base(correlationId)
    {
    }

    public SearchProductMaterialResponse()
    {
    }

    public List<ProductMaterialDto> ProductMaterials { get; set; } = new List<ProductMaterialDto>();
    public int TotalCount { get; set; }
}
