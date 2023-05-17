using System;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.Modules.MaterialSupplies.Dtos;

public class UpdateMaterialSupplyRequest : BaseRequest
{
    [Range(1, 10000)]
    public int Id { get; set; }

    [Required]
    public string DeliveredDate { get; set; }

    [Range(1, 10000)]
    public decimal TotalPrice { get; set; }

    [Required]
    public double Amount { get; set; }
}
