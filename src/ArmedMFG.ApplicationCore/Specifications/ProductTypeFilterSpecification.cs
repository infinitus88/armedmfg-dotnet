using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProductTypeFilterSpecification : Specification<ProductType>
{
    public ProductTypeFilterSpecification(string? name, int? productCategoryId)
    {
        Query.Where(p => (!String.IsNullOrEmpty(name) || p.Name.ToLower().Contains(name.ToLower())) &&
                         (!productCategoryId.HasValue || p.ProductCategoryId == productCategoryId));
    }
}
