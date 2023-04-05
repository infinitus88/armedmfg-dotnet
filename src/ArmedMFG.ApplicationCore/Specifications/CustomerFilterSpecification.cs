using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class CustomerFilterSpecification : Specification<Customer>
{
    public CustomerFilterSpecification(string? fullName)
    {
        Query.Where(c => (!String.IsNullOrEmpty(fullName) || c.FullName.ToLower().Contains(fullName.ToLower())));
    }
}
