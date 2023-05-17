using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.Orders;

public sealed class OrderWithProductsSpecification : Specification<Order>, ISingleResultSpecification
{
    public OrderWithProductsSpecification(int orderId)
    {
        Query
            .Where(o => o.Id == orderId)
            .Include(o => o.OrderProducts);
    }
}
