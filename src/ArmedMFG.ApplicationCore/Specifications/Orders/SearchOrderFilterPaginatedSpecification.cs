using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.Orders;

public class SearchOrderFilterPaginatedSpecification : Specification<Order>
{
    public SearchOrderFilterPaginatedSpecification(int skip, int take, string searchText, DateTime? startDate, DateTime? endDate)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query
            .Where(o => (!startDate.HasValue || o.OrderedDate >= startDate) &&
                        (!endDate.HasValue || o.OrderedDate <= endDate))
            .Skip(skip).Take(take)
            .Include(o => o.Customer)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.ProductType)
            .Include(o => o.OrderShipments);
    }
}
