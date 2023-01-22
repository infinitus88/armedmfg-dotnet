using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class PagedProductTypeResponse
{
    public List<ProductType> ProductTypes { get; set; } = new List<ProductType>();
    public int PageCount { get; set; } = 0;
}
