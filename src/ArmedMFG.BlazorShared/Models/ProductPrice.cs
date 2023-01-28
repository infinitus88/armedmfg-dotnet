using System;
using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class ProductPrice
{
    public int Id { get; set; }
    public int ProductTypeId { get; set; }
    public string ProductType { get; set; } = "NotSet";
    public DateTime FromDate { get; set; }
    
    [RegularExpression(@"^\d+(\.\d{0,2})*$", ErrorMessage = "The field Price must be a positive number with maximum two decimals.")]
    [Range(1000.00, 100000000.00)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; } 
}
