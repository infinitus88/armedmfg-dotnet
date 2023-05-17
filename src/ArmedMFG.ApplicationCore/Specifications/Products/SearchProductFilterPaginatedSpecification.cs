using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.Products;

public class SearchProductFilterPaginatedSpecification : Specification<Product>
{
    public SearchProductFilterPaginatedSpecification(int skip, int take, string? searchText, int? productCategoryId)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query
            .Where(t => !string.IsNullOrEmpty(searchText) || t.Name.ToLower().Contains(searchText.ToLower()))
            //(!productCategoryId.HasValue || t.ProductCategoryId == productCategoryId))
            .Skip(skip).Take(take);
    }
}
