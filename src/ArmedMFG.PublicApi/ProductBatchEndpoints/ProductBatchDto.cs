using System;
using System.Collections.Generic;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class ProductBatchDto
{
    public int Id { get; set; }
    public DateTime ProducedDate { get; set; }
    public List<ProducedProductDto> ProducedProducts { get; set; } = new List<ProducedProductDto>();
    public List<SpentMaterialDto> SpentMaterials { get; set; } = new List<SpentMaterialDto>();
}
