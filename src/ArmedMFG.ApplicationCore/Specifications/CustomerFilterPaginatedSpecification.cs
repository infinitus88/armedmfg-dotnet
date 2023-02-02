using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class CustomerFilterPaginatedSpecification : Specification<Customer>
{
    public CustomerFilterPaginatedSpecification(int skip, int take, int? organizationId)
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Where(c => (!organizationId.HasValue || c.OrganizationId == organizationId))
            .Skip(skip).Take(take);
    }
}

public class CustomerOrganizationFilterPaginatedSpecification : Specification<CustomerOrganization>
{
    public CustomerOrganizationFilterPaginatedSpecification(int skip, int take)
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query.Skip(skip).Take(take);
    }
}
