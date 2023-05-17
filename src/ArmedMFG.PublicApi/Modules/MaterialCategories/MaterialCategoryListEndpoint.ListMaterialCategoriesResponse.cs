using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.MaterialCategories;

namespace ArmedMFG.PublicApi.MaterialCategoryEndpoints;

public class ListMaterialCategoriesResponse : BaseResponse
{
    public ListMaterialCategoriesResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListMaterialCategoriesResponse()
    {
    }

    public List<MaterialCategoryDto> MaterialCategories { get; set; } = new List<MaterialCategoryDto>();
}
