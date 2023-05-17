using System;
using System.Runtime.InteropServices.JavaScript;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.Materials;

public class SearchMaterialFilterSpecification : Specification<Material>
{
    public SearchMaterialFilterSpecification(string? searchText, int materialCategoryId)
    {
        Query.Where(p => p.Name.ToLower().Contains(searchText.ToLower()));
    }
}
