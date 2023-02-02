using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.ClientAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ClientFilterPaginatedSpecification : Specification<Client>
{
    public ClientFilterPaginatedSpecification(int skip, int take, int? organizationId)
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
