namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class CreateSpentMaterialRequest
{
    public int MaterialTypeId { get; set; }
    public double Amount { get; set; }
}
