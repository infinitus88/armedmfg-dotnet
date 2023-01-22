using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities;

public class Department : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    
    public Department(string name)
    {
        Name = name;
    }
}

