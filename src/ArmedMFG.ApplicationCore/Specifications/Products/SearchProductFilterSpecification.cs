using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.Products;

public class SearchProductFilterSpecification : Specification<Product>
{
    public SearchProductFilterSpecification(string? name, int? productCategoryId)
    {
        Query.Where(p => !string.IsNullOrEmpty(name) || p.Name.ToLower().Contains(name.ToLower()));
        //(!productCategoryId.HasValue || p.ProductCategoryId == productCategoryId));
    }
}
