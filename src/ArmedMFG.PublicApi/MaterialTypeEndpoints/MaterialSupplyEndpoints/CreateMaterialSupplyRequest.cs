using System;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class CreateMaterialSupplyRequest : BaseRequest
{
    public int MaterialTypeId { get; set; }
    public DateTime DeliveredDate { get; set; }
    public decimal Price { get; set; }
    public double Amount { get; set; }
}
