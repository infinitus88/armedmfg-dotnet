using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class CustomerAutocompleteFilterSpecification : Specification<Customer>
{
    public CustomerAutocompleteFilterSpecification(string? fullName)
    {
        Query.Where(c => c.FullName.Contains(fullName));
    }
}
