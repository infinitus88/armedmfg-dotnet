using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class CustomerFilterPaginatedSpecification : Specification<Customer>
{
    public CustomerFilterPaginatedSpecification(int skip, int take, string? fullName)
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Where(c => (!String.IsNullOrEmpty(fullName) || c.FullName.ToLower().Contains(fullName.ToLower())))
            .Skip(skip).Take(take);
    }
}
