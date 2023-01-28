using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class PagedProductPriceResponse
{
    public List<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
    public int PageCount { get; set; } = 0;
}
