using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class OrderFilterSpecification : Specification<Order>
{
    public OrderFilterSpecification(int? customerId)
    {
        Query.Where(o => (!customerId.HasValue || o.CustomerId == customerId));
    }
}
