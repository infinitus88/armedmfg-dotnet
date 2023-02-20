using System;
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

public class OrderShipmentFilterPaginatedSpecification : Specification<OrderShipment>
{
    public OrderShipmentFilterPaginatedSpecification(int skip, int take, DateTime? startDate, DateTime? endDate, int? orderId)
    : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Where(o => (!startDate.HasValue || o.ShipmentDate.Date >= startDate) &&
                        (!endDate.HasValue || o.ShipmentDate <= endDate) &&
                        (!orderId.HasValue || o.OrderId == orderId))
            .Skip(skip).Take(take)
            .Include(o => o.ShipmentProducts);
    }
}
