using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class MaterialTypeNameSpecification : Specification<MaterialType>
{
    public MaterialTypeNameSpecification(string materialTypeName)
    {
        Query.Where(materialType => materialTypeName == materialType.Name);
    }
}
