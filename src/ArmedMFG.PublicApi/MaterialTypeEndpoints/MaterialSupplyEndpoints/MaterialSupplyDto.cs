using System;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class MaterialSupplyDto
{
    public int Id { get; set; }
    public DateTime DeliveredDate { get; set; }
    public decimal Price { get; set; }
    public double Amount { get; set; }
    public int MaterialTypeId { get; set; }
}
