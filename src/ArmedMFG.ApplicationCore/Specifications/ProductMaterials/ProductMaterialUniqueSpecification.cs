using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.ProductMaterials;

public class ProductMaterialUniqueSpecification : Specification<ProductMaterial>
{
    public ProductMaterialUniqueSpecification(int productId, int materialId)
    {
        Query
            .Include(p => p.ProductId == productId && materialId == p.MaterialId);
    }
}
