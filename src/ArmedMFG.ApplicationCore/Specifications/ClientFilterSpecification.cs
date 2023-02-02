using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ClientAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ClientFilterSpecification : Specification<Client>
{
    public ClientFilterSpecification(int? organizationId)
    {
        Query.Where(c => (!organizationId.HasValue || c.OrganizationId == organizationId));
    }
}
