using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;

public class ProductCategory : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public int DepartmentId { get; private set; }
    public Department? Department { get; private set; }

    public ProductCategory(int departmentId, string name)
    {
        DepartmentId = departmentId;
        Name = name;
    }
}
