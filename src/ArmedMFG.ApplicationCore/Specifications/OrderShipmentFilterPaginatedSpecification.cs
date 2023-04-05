using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class OrderShipmentFilterPaginatedSpecification : Specification<OrderShipment>
{
    public OrderShipmentFilterPaginatedSpecification(int skip, int take, DateTime? startDate, DateTime? endDate, string customerName, string? driverName, string? carNumber)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Include(o => o.Order)
            .ThenInclude(o => o.Customer)
            .Where(o => ((!startDate.HasValue || o.ShipmentDate.Date >= startDate) &&
                         (!endDate.HasValue || o.ShipmentDate <= endDate)) &&
                         (o.Order.Customer.FullName.ToLower().Contains(customerName.ToLower())) ||
                         (o.DriverName.ToLower().Contains(driverName.ToLower())) ||
                         (o.CarNumber.ToLower().Contains(carNumber.ToLower())))
            .Skip(skip).Take(take);
    }
}
