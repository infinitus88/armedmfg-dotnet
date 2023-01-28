using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class MaterialSupplyFilterSpecification : Specification<MaterialSupply>
{
    public MaterialSupplyFilterSpecification(int? materialTypeId)
    {
        Query.Where(p => (!materialTypeId.HasValue || p.MaterialTypeId == materialTypeId));
    }
}
