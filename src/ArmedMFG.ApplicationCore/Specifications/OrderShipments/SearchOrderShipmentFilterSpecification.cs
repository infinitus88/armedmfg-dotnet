using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.OrderShipments;

public class SearchOrderShipmentFilterSpecification : Specification<OrderShipment>
{
    public SearchOrderShipmentFilterSpecification(DateTime? startDate, DateTime? endDate, int orderId, string searchText)
    {
        Query
            .Include(o => o.Order)
            .ThenInclude(o => o.Customer)
            .Where(o => (!startDate.HasValue || o.ShipmentDate <= startDate) &&
                        (!endDate.HasValue || o.ShipmentDate >= endDate) &&
                        o.OrderId == orderId);

    }

    public SearchOrderShipmentFilterSpecification()
    {
        Query.OrderBy(o => o.ShipmentDate);
    }
}
