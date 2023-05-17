using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.ProductAggregate;

public class ProductCategory : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }

    public ProductCategory(string name)
    {
        Name = name;
    }
}
