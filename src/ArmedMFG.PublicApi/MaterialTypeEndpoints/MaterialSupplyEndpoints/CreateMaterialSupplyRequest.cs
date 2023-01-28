using System;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class CreateMaterialSupplyRequest : BaseRequest
{
    public int MaterialTypeId { get; set; }
    public DateTime DeliveredDate { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Amount { get; set; }
}
