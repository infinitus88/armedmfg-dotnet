using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class EmployeeFilterSpecification : Specification<Employee>
{
    public EmployeeFilterSpecification(string searchText, int positionId, byte status)
    {
        Query
            .Where(e => (!String.IsNullOrEmpty(searchText) || e.FullName.ToLower().Contains(searchText.ToLower())));
                        //(positionId != 0 || e.PositionId == positionId));
                        //(e.Status == (EmployeeStatus)status));
    }
}
