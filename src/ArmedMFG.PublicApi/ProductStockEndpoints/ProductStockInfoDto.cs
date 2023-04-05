namespace ArmedMFG.PublicApi.ProductStockEndpoints;

public class ProductStockInfoDto
{
    public int ProductTypeId { get; set; }
    public string? ProductName { get; set; }
    public int ProductCategoryId { get; set; }
    public int Quantity { get; set; }
}
