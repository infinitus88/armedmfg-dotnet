using System;
using System.Collections.Generic;
using ArmedMFG.PublicApi.Modules.ProductCategoryEndpoints;

namespace ArmedMFG.PublicApi.ProductCategoryEndpoints;

public class ListProductCategoriesResponse : BaseResponse
{
    public ListProductCategoriesResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListProductCategoriesResponse()
    {
    }

    public List<ProductCategoryDto> ProductCategories { get; set; } = new List<ProductCategoryDto>();
}
