using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class SpentMaterial
{
    public int Id { get; set; }
    public int MaterialTypeId { get; set; }
    public string MaterialType { get; set; } = "NotSet";
    [RegularExpression(@"^\d+(\.\d{0,2})*$",
        ErrorMessage = "The field Amount must be a positive number with maximum two decimals.")]
    [Range(1.00, 1000000.00)] 
    public decimal Amount { get; set; }
}
