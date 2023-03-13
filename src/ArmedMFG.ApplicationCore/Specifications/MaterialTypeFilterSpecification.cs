using System;
using System.Runtime.InteropServices.JavaScript;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class MaterialTypeFilterSpecification : Specification<MaterialType>
{
    public MaterialTypeFilterSpecification(string? name)
    {
        Query.Where(p => (p.Name.ToLower().Contains(name.ToLower())));
    }
}
