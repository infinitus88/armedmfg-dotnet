using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class OrderShipmentFilterSpecification : Specification<OrderShipment>
{
    public OrderShipmentFilterSpecification(DateTime? startDate, DateTime? endDate, int? orderId)
    {
        Query.Where(o => (!startDate.HasValue || o.ShipmentDate <= startDate) &&
                         (!endDate.HasValue || o.ShipmentDate >= endDate) &&
                         (!orderId.HasValue || o.OrderId == orderId));
    }
}
