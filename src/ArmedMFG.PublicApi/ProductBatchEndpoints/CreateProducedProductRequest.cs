namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class CreateProducedProductRequest
{
    public int ProductTypeId { get; set; }
    public int Quantity { get; set; }
}
