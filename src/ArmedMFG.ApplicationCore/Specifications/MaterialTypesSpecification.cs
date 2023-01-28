using System.Linq;
using Ardalis.Specification;
using MaterialType = ArmedMFG.BlazorShared.Models.MaterialType;

namespace ArmedMFG.ApplicationCore.Specifications;

public class MaterialTypesSpecification : Specification<MaterialType>
{
    public MaterialTypesSpecification(params int[] ids)
    {
        Query.Where(p => ids.Contains(p.Id));
    }
}
