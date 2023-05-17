using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.MaterialSupplies;

public class SearchMaterialSupplyFilterSpecification : Specification<MaterialSupply>
{
    public SearchMaterialSupplyFilterSpecification(int? materialId)
    {
        Query.Where(p => !materialId.HasValue || p.MaterialId == materialId);
    }

    public SearchMaterialSupplyFilterSpecification()
    {
    }
}
