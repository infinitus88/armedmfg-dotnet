using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class CustomerFilterSpecification : Specification<Customer>
{
    public CustomerFilterSpecification(int? organizationId)
    {
        Query.Where(c => (!organizationId.HasValue || c.OrganizationId == organizationId));
    }
}
