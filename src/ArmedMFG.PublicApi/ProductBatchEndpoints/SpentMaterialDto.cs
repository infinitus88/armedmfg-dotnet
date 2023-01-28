namespace ArmedMFG.PublicApi.ProductBatchEndpoints;

public class SpentMaterialDto
{
    public int Id { get; set; }
    public int MaterialTypeId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Amount { get; set; }
}
