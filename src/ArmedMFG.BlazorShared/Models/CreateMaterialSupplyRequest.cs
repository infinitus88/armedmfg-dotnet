using System;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class CreateMaterialSupplyRequest
{
    public int MaterialTypeId { get; set; }
    
    public DateTime DeliveredDate { get; set; }
    
    [RegularExpression(@"^\d+(\.\d{0,2})*$",
        ErrorMessage = "The field Unit Price must be a positive number with maximum two decimals.")]
    [Range(100000.00, 1000000.00)] 
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    
    [RegularExpression(@"^\d+(\.\d{0,2})*$",
        ErrorMessage = "The field Amount must be a positive number with maximum two decimals.")]
    [Range(100.0, 1000000.0)] 
    public double Amount { get; set; }

}
