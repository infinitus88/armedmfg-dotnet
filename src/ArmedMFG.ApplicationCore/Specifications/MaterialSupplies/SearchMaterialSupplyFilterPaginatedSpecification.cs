using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.MaterialSupplies;

public class SearchMaterialSupplyFilterPaginatedSpecification : Specification<MaterialSupply>
{
    public SearchMaterialSupplyFilterPaginatedSpecification(int skip, int take, int? materialId)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query
            .Where(t => !materialId.HasValue || t.MaterialId == materialId)
            .Skip(skip).Take(take);
    }
}
