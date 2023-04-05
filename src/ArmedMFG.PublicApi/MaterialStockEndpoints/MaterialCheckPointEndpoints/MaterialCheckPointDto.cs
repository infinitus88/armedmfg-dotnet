using System;

namespace ArmedMFG.PublicApi.MaterialStockEndpoints.MaterialCheckPointEndpoints;

public class MaterialCheckPointDto
{
    public int Id { get; set; }
    public DateTime CheckedDate { get; set; }
    public int MaterialTypeId { get; set; }
    public string? MaterialName { get; set; }
    public int MaterialCategoryId { get; set; }
    public double Amount { get; set; }
}
