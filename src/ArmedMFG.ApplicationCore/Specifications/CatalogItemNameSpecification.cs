using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using CatalogItem = ArmedMFG.ApplicationCore.Entities.CatalogItem;

namespace ArmedMFG.ApplicationCore.Specifications;

public class CatalogItemNameSpecification : Specification<CatalogItem>
{
    public CatalogItemNameSpecification(string catalogItemName)
    {
        Query.Where(item => catalogItemName == item.Name);
    }
}
