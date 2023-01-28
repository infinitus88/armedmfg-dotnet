using System;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class CreateProductPriceRequest
{
    public int ProductTypeId { get; set; }
    public DateTime FromDate { get; set; }
    
    // decimal(18,2)
    [RegularExpression(@"^\d+(\.\d{0,2})*$",
        ErrorMessage = "The field Price must be a positive number with maximum two decimals.")]
    [Range(1000.00, 10000000.00)] 
    [DataType(DataType.Currency)]
    public decimal Price { get; set; } = 0;
}
