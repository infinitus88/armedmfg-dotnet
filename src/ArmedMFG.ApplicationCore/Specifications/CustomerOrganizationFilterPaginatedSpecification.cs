using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class CustomerOrganizationFilterPaginatedSpecification : Specification<CustomerOrganization>
{
    public CustomerOrganizationFilterPaginatedSpecification(int skip, int take, string? searchText)
    {
        Query
            .Where(o => (!String.IsNullOrWhiteSpace(searchText) || o.Name.ToLower().Contains(searchText.ToLower())))
            .Include(p => p.MainBranchAddress)
            .Skip(skip).Take(take);
    }

    public CustomerOrganizationFilterPaginatedSpecification(int skip, int take)
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query.Skip(skip).Take(take);
    }
}
