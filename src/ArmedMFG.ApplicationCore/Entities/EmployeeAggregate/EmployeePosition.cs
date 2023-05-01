using ArmedMFG.ApplicationCore.Interfaces;

namespace ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;

public class EmployeePosition : BaseEntity, IAggregateRoot
{
    public string Name { get; set; }
    public EmployeeSalaryType SalaryType { get; set; }

    public EmployeePosition(string name, EmployeeSalaryType salaryType)
    {
        Name = name;
        SalaryType = salaryType;
    }
}

public enum EmployeeSalaryType : byte
{
    Fixed = 0,
    PerformanceBased = 1
}
