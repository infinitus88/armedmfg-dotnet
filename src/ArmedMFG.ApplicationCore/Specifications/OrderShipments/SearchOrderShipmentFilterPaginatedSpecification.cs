using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.OrderShipments;

public class SearchOrderShipmentFilterPaginatedSpecification : Specification<OrderShipment>
{
    public SearchOrderShipmentFilterPaginatedSpecification(int skip, int take, DateTime? startDate, DateTime? endDate, int orderId, string searchText)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Include(o => o.Order)
            .ThenInclude(o => o.Customer)
            .Where(o => (!startDate.HasValue || o.ShipmentDate.Date >= startDate) &&
                         (!endDate.HasValue || o.ShipmentDate <= endDate) &&
                         o.OrderId == orderId)
            .Skip(skip).Take(take);
    }
}
