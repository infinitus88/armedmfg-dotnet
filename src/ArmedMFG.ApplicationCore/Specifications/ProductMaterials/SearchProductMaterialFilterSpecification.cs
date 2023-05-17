using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using ArmedMFG.ApplicationCore.Entities.ProductionReport;

namespace ArmedMFG.ApplicationCore.Specifications.ProductMaterials;
public class SearchProductMaterialFilterSpecification : Specification<ProductMaterial>
{
    public SearchProductMaterialFilterSpecification(int productId, string searchText)
    {
        Query
            .Include(p => p.Material)
            .Where(p => p.ProductId == productId && p.Material.Name.ToLower().Contains(searchText.ToLower()));
    }
}
