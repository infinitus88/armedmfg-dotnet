using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class MaterialTypeFilterPaginatedSpecification : Specification<MaterialType>
{
    public MaterialTypeFilterPaginatedSpecification(int skip, int take, string? name)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Where(t => (t.Name.ToLower().Contains(name.ToLower())))
            .Skip(skip).Take(take);
        // .Include(t => t.MaterialSupplies);
    }
}
