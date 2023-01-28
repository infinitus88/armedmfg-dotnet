using System;

namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class ProducedProductDto
{
    public int Id { get; set; }
    public int ProductTypeId { get; set; }
    public int Quantity { get; set; }
}
