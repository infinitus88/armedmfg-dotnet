using ArmedMFG.BlazorShared.Attributes;

namespace ArmedMFG.BlazorShared.Models;

[Endpoint(Name = "material-categories")]
public class MaterialCategory : LookupData
{
    public int DepartmentId { get; set; }
}
