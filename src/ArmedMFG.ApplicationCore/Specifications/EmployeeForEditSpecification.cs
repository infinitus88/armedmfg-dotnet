using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public sealed class EmployeeForEditSpecification : Specification<Employee>, ISingleResultSpecification
{
    public EmployeeForEditSpecification(int employeeId)
    {
        Query
            .Where(e => e.Id == employeeId);
    }
}
