using System;
using System.Globalization;
using System.Linq;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Entities.WarehouseAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class OrderShipmentFilterSpecification : Specification<OrderShipment>
{
    public OrderShipmentFilterSpecification(DateTime? startDate, DateTime? endDate, int? orderId)
    {
        Query.Where(o => (!startDate.HasValue || o.ShipmentDate <= startDate) &&
                         (!endDate.HasValue || o.ShipmentDate >= endDate) &&
                         (!orderId.HasValue || o.OrderId == orderId))
            .Include(o => o.ShipmentProducts);
    }

    public OrderShipmentFilterSpecification(DateTime? startDate)
    {
        Query.Where(o => (!startDate.HasValue || o.ShipmentDate <= startDate))
            .Include(o => o.ShipmentProducts);
    }
}

public class WarehouseProductCheckPointRecentSpecification : Specification<WarehouseProductCheckPoint>
{
    public WarehouseProductCheckPointRecentSpecification()
    {
        Query.OrderBy(cp => cp.CheckedDate);
    }
}
