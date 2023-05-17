using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.Materials;

public class MaterialNameSpecification : Specification<Material>
{
    public MaterialNameSpecification(string materialTypeName)
    {
        Query.Where(materialType => materialTypeName == materialType.Name);
    }
}
