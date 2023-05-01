using System.Collections.Generic;

namespace ArmedMFG.PublicApi.Configuration.Services;

public class SpentMaterialData
{
    public int MaterialTypeId { get; set; }
    public double Amount { get; set; }
}

public class InventoryData
{
    public List<SpentMaterialData> Materials { get; set; }
}

public class Inventory
{
    public Dictionary<int, InventoryData> Data { get; set; }
}
