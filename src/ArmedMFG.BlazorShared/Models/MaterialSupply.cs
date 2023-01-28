using System;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class MaterialSupply
{
    public int Id { get; set; }
    public int MaterialTypeId { get; set; }
    public string MaterialType { get; set; } = "NotSet";
    
    public DateTime DeliveredDate { get; set; }
    
    [RegularExpression(@"^\d+(\.\d{0,2})*$",
        ErrorMessage = "The field Unit Price must be a positive number with maximum two decimals.")]
    [Range(1.00, 1000000.00)] 
    [DataType(DataType.Currency)]
    public decimal UnitPrice { get; set; }
    
    [RegularExpression(@"^\d+(\.\d{0,2})*$",
        ErrorMessage = "The field Amount must be a positive number with maximum two decimals.")]
    [Range(1.00, 1000000.00)] 
    public decimal Amount { get; set; }
}
