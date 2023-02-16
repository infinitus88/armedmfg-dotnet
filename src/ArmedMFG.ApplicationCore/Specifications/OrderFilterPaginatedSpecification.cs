using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class OrderFilterPaginatedSpecification : Specification<Order>
{
    public OrderFilterPaginatedSpecification(int skip, int take, int? customerId)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Where(o => (!customerId.HasValue || o.CustomerId == customerId))
            .Skip(skip).Take(take)
            .Include(o => o.OrderProducts)
            .Include(o => o.OrderShipments);
    }
}
