﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.PublicApi.MaterialTypeEndpoints.MaterialSupplyEndpoints;

public class UpdateMaterialSupplyRequest : BaseRequest
{
    [Range(1, 10000)]
    public int Id { get; set; }
    
    [Range(1, 10000)]
    public int MaterialTypeId { get; set; }
    
    [Required]
    public DateTime DeliveredDate { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public double Amount { get; set; }
}
