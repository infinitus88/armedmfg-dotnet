using System.Linq;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.MaterialSupplies;

public class MaterialSuppliesSpecification : Specification<MaterialSupply>
{
    public MaterialSuppliesSpecification(params int[] ids)
    {
        Query.Where(p => ids.Contains(p.Id));
    }
}
