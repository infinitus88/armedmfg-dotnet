using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using CatalogItem = ArmedMFG.ApplicationCore.Entities.CatalogItem;

namespace ArmedMFG.ApplicationCore.Specifications;

public class CatalogFilterSpecification : Specification<CatalogItem>
{
    public CatalogFilterSpecification(int? brandId, int? typeId)
    {
        Query.Where(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
            (!typeId.HasValue || i.CatalogTypeId == typeId));
    }
}
