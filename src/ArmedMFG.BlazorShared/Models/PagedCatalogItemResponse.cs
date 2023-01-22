using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class PagedCatalogItemResponse
{
    public List<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
    public int PageCount { get; set; } = 0;
}
