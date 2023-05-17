using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.CustomerAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.Customers;

public class SearchCustomerFilterSpecification : Specification<Customer>
{
    public SearchCustomerFilterSpecification(string? fullName)
    {
        Query.Where(c => !string.IsNullOrEmpty(fullName) || c.FullName.ToLower().Contains(fullName.ToLower()));
    }
}

public class CustomerOptionsFilterSpecification : Specification<Customer>
{
    public CustomerOptionsFilterSpecification(string? fullName)
    {
        Query.Where(c => !string.IsNullOrEmpty(fullName) || c.FullName.ToLower().Contains(fullName.ToLower()));
    }
}
