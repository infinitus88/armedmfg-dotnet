using System;

namespace ArmedMFG.PublicApi.ProductStockEndpoints.ProductCheckpointEndpoints;

public class ProductCheckPointDto
{
    public int Id { get; set; }
    public DateTime CheckedDate { get; set; }
    public int ProductTypeId { get; set; }
    public string? ProductName { get; set; }
    public int ProductCategoryId { get; set; }
    public int Quantity { get; set; }
}
