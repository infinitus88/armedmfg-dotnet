using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class EmployeeFilterPaginatedSpecification : Specification<Employee>
{
    public EmployeeFilterPaginatedSpecification(int skip, int take, string searchText, int positionId, byte status) : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query
            .Where(e => (!String.IsNullOrEmpty(searchText) || e.FullName.ToLower().Contains(searchText.ToLower())))
                        //(positionId != 0 || e.PositionId == positionId))
                        //(e.Status == (EmployeeStatus)status))
            .Skip(skip).Take(take)
            .Include(e => e.Position);
    }
}
