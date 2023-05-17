using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.CustomerAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.Customers;

public class SearchCustomerFilterPaginatedSpecification : Specification<Customer>
{
    public SearchCustomerFilterPaginatedSpecification(int skip, int take, string? fullName)
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Where(c => !string.IsNullOrEmpty(fullName) || c.FullName.ToLower().Contains(fullName.ToLower()))
            .Skip(skip).Take(take);
    }
}
