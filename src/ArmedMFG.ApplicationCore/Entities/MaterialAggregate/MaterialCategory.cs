using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.MaterialAggregate;

public class MaterialCategory : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }

    public MaterialCategory(string name)
    {
        Name = name;
    }
}
