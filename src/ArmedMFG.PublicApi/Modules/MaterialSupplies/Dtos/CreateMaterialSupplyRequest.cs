using System;

namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos;

public class CreateMaterialSupplyRequest : BaseRequest
{
    public int MaterialId { get; set; }
    public string DeliveredDate { get; set; }
    public decimal TotalPrice { get; set; }
    public double Amount { get; set; }
}
