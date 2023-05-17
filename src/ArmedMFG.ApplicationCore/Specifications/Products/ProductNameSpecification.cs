using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ProductAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.Products;

public class ProductNameSpecification : Specification<Product>
{
    public ProductNameSpecification(string productName)
    {
        Query.Where(product => productName == product.Name);
    }
}
