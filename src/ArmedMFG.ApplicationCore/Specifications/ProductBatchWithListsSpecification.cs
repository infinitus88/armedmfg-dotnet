using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductBatch;

namespace ArmedMFG.ApplicationCore.Specifications;

public sealed class ProductBatchWithListsSpecification : Specification<ProductBatch>, ISingleResultSpecification
{
    public ProductBatchWithListsSpecification(int productBatchId)
    {
        Query
            .Where(b => b.Id == productBatchId)
            .Include(b => b.ProducedProducts)
            .Include(b => b.SpentMaterials);
    }
}
