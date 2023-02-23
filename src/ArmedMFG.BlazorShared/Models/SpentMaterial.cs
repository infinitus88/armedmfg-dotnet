using System.ComponentModel.DataAnnotations;

namespace ArmedMFG.BlazorShared.Models;

public class SpentMaterial
{
    public int Id { get; set; }
    public int MaterialTypeId { get; set; }
    public string MaterialType { get; set; } = "NotSet";
    [Range(1.0, 1000000.0)] 
    public double Amount { get; set; }
}
