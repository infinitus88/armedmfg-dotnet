using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class PagedProductBatchResponse
{
    public List<ProductBatch> ProductBatches { get; set; } = new List<ProductBatch>();
    public int PageCount { get; set; } = 0;
}
