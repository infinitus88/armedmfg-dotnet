using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.Materials;

public class SearchMaterialFilterPaginatedSpecification : Specification<Material>
{
    public SearchMaterialFilterPaginatedSpecification(int skip, int take, string? searchText, int materialCategoryId)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Where(t => t.Name.ToLower().Contains(searchText.ToLower()))
            .Skip(skip).Take(take);
        // .Include(t => t.MaterialSupplies);
    }
}
