using ArmedMFG.BlazorShared.Attributes;

namespace ArmedMFG.BlazorShared.Models;

[Endpoint(Name = "material-category")]
public class MaterialCategory : LookupData
{
    public int DepartmentId { get; set; }
}
