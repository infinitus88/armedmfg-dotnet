using System.Linq;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.ProductMaterials;

public class SearchProductMaterialFilterPaginatedSpecification : Specification<ProductMaterial>
{
    public SearchProductMaterialFilterPaginatedSpecification(int skip, int take, int productId, string searchText)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Include(p => p.Material)
            .Where(p => (p.ProductId == productId && p.Material.Name.ToLower().Contains(searchText.ToLower())))
            .Skip(skip).Take(take);
    }
}
