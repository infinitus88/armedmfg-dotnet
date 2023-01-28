using System;
using System.Collections.Generic;

namespace ArmedMFG.BlazorShared.Models;

public class ProductBatch
{
    public int Id { get; set; }
    public DateTime ProducedDate { get; set; }
    public List<ProducedProduct> ProducedProducts { get; set; }
    public List<SpentMaterial> SpentMaterials { get; set; }
}
