using System.Linq;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class MaterialSuppliesSpecification : Specification<MaterialSupply>
{
    public MaterialSuppliesSpecification(params int[] ids)
    {
        Query.Where(p => ids.Contains(p.Id));
    }
}
