using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class OrderProductFilterSpecification : Specification<OrderProduct>
{
    public OrderProductFilterSpecification()
    {
        Query
            .Include(op => op.Order)
            .OrderBy(op => op.Order.OrderedDate);
    }
}
