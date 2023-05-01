using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class CustomerOrganizationFilterSpecification : Specification<CustomerOrganization>
{
    public CustomerOrganizationFilterSpecification(string? searchText)
    {
        Query.Where(o => (!String.IsNullOrWhiteSpace(searchText) || o.Name.ToLower().Contains(searchText.ToLower())));
    }
}
