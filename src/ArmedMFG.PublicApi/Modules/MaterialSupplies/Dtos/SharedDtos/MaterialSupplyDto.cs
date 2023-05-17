using System;

namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos.SharedDtos;

public class MaterialSupplyDto
{
    public int Id { get; set; }
    public DateTime DeliveredDate { get; set; }
    public int MaterialId { get; set; }
    public decimal TotalPrice { get; set; }
    public double Amount { get; set; }
}
