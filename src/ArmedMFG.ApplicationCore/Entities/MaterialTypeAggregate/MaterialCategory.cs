using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;

public class MaterialCategory : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public int DepartmentId { get; private set; }
    public Department? Department { get; private set; }

    public MaterialCategory(int departmentId, string name)
    {
        DepartmentId = departmentId;
        Name = name;
    }
}
