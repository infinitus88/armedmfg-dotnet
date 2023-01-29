using System.Collections.Generic;
using ArmedMFG.BlazorShared.Attributes;

namespace ArmedMFG.BlazorShared.Models;

[Endpoint(Name = "product-types")]
public class PagedCatalogItemResponse
{
    public List<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
    public int PageCount { get; set; } = 0;
}
