using System.Linq;
using Ardalis.Specification;
using ArmedMFG.BlazorShared.Models;

namespace ArmedMFG.ApplicationCore.Specifications;

public class MaterialTypesSpecification : Specification<MaterialType>
{
    public MaterialTypesSpecification(params int[] ids)
    {
        Query.Where(p => ids.Contains(p.Id));
    }
}
