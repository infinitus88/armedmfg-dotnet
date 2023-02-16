using System;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints.ProducedProductEndpoints;

public class ProducedProductQuantityDto
{
    public int ProductTypeId { get; set; }
    public int Quantity { get; set; }
}
