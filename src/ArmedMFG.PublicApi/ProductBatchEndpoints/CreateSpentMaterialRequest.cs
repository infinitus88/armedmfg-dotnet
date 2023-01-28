namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class CreateSpentMaterialRequest
{
    public int MaterialTypeId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Amount { get; set; }
}
