namespace ArmedMFG.PublicApi.MaterialStockEndpoints;

public class MaterialStockInfoDto
{
    public int MaterialTypeId { get; set; }
    public string? MaterialName { get; set; }
    public int MaterialCategoryId { get; set; }
    public double Amount { get; set; }
}
