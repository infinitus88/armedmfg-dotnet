using System;
using System.Linq;
using Ardalis.Specification;
using CatalogItem = ArmedMFG.ApplicationCore.Entities.CatalogItem;

namespace ArmedMFG.ApplicationCore.Specifications;

public class CatalogItemsSpecification : Specification<CatalogItem>
{
    public CatalogItemsSpecification(params int[] ids)
    {
        Query.Where(c => ids.Contains(c.Id));
    }
}
