using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class PagedMaterialSupplyResponse
{
    public List<MaterialSupply> MaterialSupplies { get; set; } = new List<MaterialSupply>();
    public int PageCount { get; set; } = 0;
}
