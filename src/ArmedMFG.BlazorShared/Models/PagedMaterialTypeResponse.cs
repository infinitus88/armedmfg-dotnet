using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class PagedMaterialTypeResponse
{
    public List<MaterialType> MaterialTypes { get; set; } = new List<MaterialType>();
    public int PageCount { get; set; } = 0;
}
