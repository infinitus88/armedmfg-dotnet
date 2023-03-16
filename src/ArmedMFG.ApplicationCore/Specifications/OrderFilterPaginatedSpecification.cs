using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class OrderFilterPaginatedSpecification : Specification<Order>
{
    public OrderFilterPaginatedSpecification(int skip, int take, DateTime? startDate, DateTime? endDate, string? customerName)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Where(o => (!startDate.HasValue || o.OrderedDate >= startDate) &&
                        (!endDate.HasValue || o.OrderedDate <= endDate) &&
                        (!String.IsNullOrEmpty(customerName) || o.Customer.FullName.ToLower().Contains(customerName.ToLower())))
            .Skip(skip).Take(take)
            .Include(o => o.Customer)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.ProductType)
            .Include(o => o.OrderShipments);
    }
}
