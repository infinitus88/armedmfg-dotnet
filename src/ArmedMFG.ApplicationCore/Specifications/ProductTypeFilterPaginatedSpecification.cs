using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ProductTypeFilterPaginatedSpecification : Specification<ProductType>
{
    public ProductTypeFilterPaginatedSpecification(int skip, int take, string? searchText, int? productCategoryId)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query
            .Where(t => (!String.IsNullOrEmpty(searchText) || t.Name.ToLower().Contains(searchText.ToLower()))) 
                        //(!productCategoryId.HasValue || t.ProductCategoryId == productCategoryId))
            .Skip(skip).Take(take)
            .Include(t => t.ProductPrices);
    }
}
